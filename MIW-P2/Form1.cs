﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MIW_P2
{
    public partial class Form1 : Form
    {
        private Dataset dataset = new Dataset();
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            textBoxDatasetPath.Text = openFileDialog1.FileName;
        }

        private void buttonLoadData_Click(object sender, EventArgs e)
        {
            if (textBoxDatasetDelimiter.Text.Length == 0)
            {
                MessageBox.Show("Please specify the dataset delimiter");
            }
            else if (textBoxDatasetPath.Text.Length == 0)
            {
                MessageBox.Show("Please specify the dataset location");
            }
            else
            {

                string[] lines = File.ReadAllLines(textBoxDatasetPath.Text);
                for (int n = 0; n < lines.Length; n++)
                {
                    string[] columns = lines[n].Split(textBoxDatasetDelimiter.Text);
                    //check type and load
                    for (int i = 0; i < columns.Length; i++)
                    {
                        float f;
                        if (float.TryParse(columns[i], out f))
                        {

                            if (n == 0)
                            {
                                textBoxLog.Text += $"Column nr {i} is numeric.{Environment.NewLine}";
                                dataset.attributes.Add(new List<object>(lines.Length) { f });
                                dataset.attributeTypes.Add("numeric");
                            }
                            else
                            {
                                dataset.attributes[i].Add(f);
                            }
                        }
                        else if (Char.IsSymbol(columns[i], 0))
                        {
                            if (n == 0)
                            {
                                textBoxLog.Text += $"Column nr {i} is a symbol.{Environment.NewLine}";
                                dataset.attributes.Add(new List<object>(lines.Length) { columns[i] });
                                dataset.attributeTypes.Add("symbol");
                            }
                            else
                            {
                                dataset.attributes[i].Add(columns[i]);
                            }
                        }
                        else
                        {

                            if (n == 0)
                            {
                                textBoxLog.Text += $"Column nr {i} is a string.{Environment.NewLine}";
                                dataset.attributes.Add(new List<object>(lines.Length) { columns[i] });
                                dataset.attributeTypes.Add("string");
                            }
                            else
                            {
                                dataset.attributes[i].Add(columns[i]);
                            }
                        }
                    }
                }
                CalculateRangeForDataset();
                textBoxLog.Text += $"Dataset loaded.{Environment.NewLine}";
                buttonLoadData.Enabled = false;
                buttonLoadData.Text = "Dataset loaded";
                buttonClassify.Enabled = true;
                buttonClearData.Enabled = true;
                NormalizeData();
                ScrollLogToBottom();
            }
        }

        void CalculateRangeForDataset()
        {
            for (int i = 0; i < dataset.attributes.Count; i++)
            {
                List<float> columnRange = new List<float>();
                if (dataset.attributeTypes[i] == "numeric")
                {
                    List<float> tmp = new List<float>();
                    foreach (var x in dataset.attributes[i])
                    {
                        if (Convert.ToString(x) == "?")
                        {
                            tmp.Add(tmp[0]);
                        }
                        else
                        {
                            tmp.Add(Convert.ToSingle(x));
                        }
                    }

                    var min = tmp.Min();
                    var max = tmp.Max();
                    columnRange.Add(min);
                    columnRange.Add(max);
                }
                else
                {
                    columnRange.Add(0);
                    columnRange.Add(0);
                }
                dataset.attributeRanges.Add(columnRange);
            }
        }

        private void buttonClearData_Click(object sender, EventArgs e)
        {
            dataset = new Dataset();
            buttonLoadData.Enabled = true;
            buttonLoadData.Text = "Load data";
            buttonClearData.Enabled = false;
            buttonClassify.Enabled = false;
            textBoxLog.Text += $"Data cleared from memory.{Environment.NewLine}";
        }

        private void ScrollLogToBottom()
        {
            textBoxLog.SelectionStart = textBoxLog.Text.Length;
            textBoxLog.ScrollToCaret();
        }

        private void NormalizeData()
        {
            List<List<double>> normalizedAttributes = new List<List<double>>(dataset.attributes.Count);
            for (int i = 0; i < dataset.attributes.Count; i++)
            {
                if (dataset.attributeTypes[i] == "numeric")
                {
                    List<double> tmpList = new List<double>();
                    foreach (var f in dataset.attributes[i])
                    {
                        if (Convert.ToString(f) == "?")
                        {
                            tmpList.Add(tmpList[0]);
                        }
                        else
                        {
                            tmpList.Add(Convert.ToSingle(f));
                        }
                    }
                    double maximum = (double)tmpList.Max();
                    double minimum = (double)tmpList.Min();
                    List<double> normalizedColumn = new List<double>(dataset.attributes[i].Count);
                    for (int j = 0; j < dataset.attributes[i].Count; j++)
                    {
                        double normalizedValue = ((double)tmpList[j] - minimum) / (maximum - minimum);
                        normalizedColumn.Add(normalizedValue);
                    }
                    normalizedAttributes.Add(normalizedColumn);
                    //add placeholder for string assignment
                    Dictionary<string, double> placeholder = new Dictionary<string, double>();
                    placeholder.Add("0",0);
                    dataset.stringAssignmentValues.Add(placeholder);
                }
                else
                {
                    List<string> uniqueStrings = CreateUniqueList(dataset.attributes[i]);
                    int length = uniqueStrings.Count;
                    List<double> normalizedColumn = new List<double>();
                    Dictionary<string, double> stringAssignment = new Dictionary<string, double>();
                    foreach (var x in dataset.attributes[i])
                    {
                        double tmp;
                        if (Convert.ToString(x) == "?")
                        {
                            tmp = 0;
                        }
                        else
                        {
                            string str = Convert.ToString(x);
                            tmp = uniqueStrings.IndexOf(str) / ((double)length - 1);
                        }
                        normalizedColumn.Add(tmp);
                    }

                    //add assigned value to string assignment values list
                    foreach (var uniqueString in uniqueStrings)
                    {
                        if (!stringAssignment.ContainsKey(uniqueString))
                        {
                            stringAssignment.Add(uniqueString, uniqueStrings.IndexOf(uniqueString) / ((double)length - 1));
                        }
                    }
                    dataset.stringAssignmentValues.Add(stringAssignment);
                    normalizedAttributes.Add(normalizedColumn);
                }
            }

            dataset.normalizedAttributes = normalizedAttributes;
            textBoxLog.Text += $"Dataset normalized.{Environment.NewLine}";
            ScrollLogToBottom();
        }
        public List<string> CreateUniqueList(List<object> list)
        {
            List<string> result = new List<string>();
            for (int i = 0; i < list.Count; i++)
            {
                if (Convert.ToString(list[i]) != "?" && !result.Contains(list[i]))
                {
                    result.Add(Convert.ToString(list[i]));
                }
            }
            return result;
        }

        private void buttonClassify_Click(object sender, EventArgs e)
        {
            string[] dataStringArray = textBoxDataToClassify.Text.Trim().Split(" ");
            if (dataStringArray.Length != dataset.attributes.Count - 1)
            {
                MessageBox.Show($"Wrong classification data length (Length should be {dataset.attributes.Count-1})");
            }
            else if (textBoxK.Text.Length == 0)
            {
                MessageBox.Show("Please specify k parameter");
            }
            else
            {
                // TODO NORMALIZE OBJ TO CLASSIFY BEFORE CLASSIFICATION
                //Convert string array to List<double>
                List<double> objectToClassify = new List<double>();
                for (int i = 0; i < dataStringArray.Length; i++)
                {
                    if (dataset.attributeTypes[i] == "numeric")
                    {
                        double tmp = Double.Parse(dataStringArray[i]);
                        tmp = (tmp - dataset.attributeRanges[i][0]) / (dataset.attributeRanges[i][1] - dataset.attributeRanges[i][0]);
                        objectToClassify.Add(tmp);
                    }
                    else
                    {
                        double tmp = dataset.stringAssignmentValues[i][dataStringArray[i]];
                        objectToClassify.Add(tmp);
                    }
                }
                //Calculate metric values
                List<List<double>> transposedList = TransposeList(dataset.normalizedAttributes);
                List<List<double>> calculatedMetricValues = new List<List<double>>();
                foreach (var classifiedObject in transposedList)
                {
                    List<double> tmp = new List<double>();
                    switch (comboBox1.Text)
                    {
                        case "Manhattan":
                            tmp = Manhattan(classifiedObject, objectToClassify);
                            break;
                        case "Czybyszew":
                            tmp = Czybyszew(classifiedObject, objectToClassify);
                            break;
                        case "Minkowski":
                            tmp = Minkowski(classifiedObject, objectToClassify);
                            break;
                        case "Logarithm":
                            tmp = Logarithm(classifiedObject, objectToClassify);
                            break;
                        default:
                            tmp = Euclidean(classifiedObject, objectToClassify);
                            break;
                    }
                    calculatedMetricValues.Add(tmp);
                }
                //Classify object
                //METHOD 1 (TAKE k SMALLEST VALUES FROM CALCULATED METRIC VALUES)
                if (radioButton1.Checked)
                {
                    if (Int32.Parse(textBoxK.Text) > calculatedMetricValues.Count || Int32.Parse(textBoxK.Text) <= 0)
                    {
                        MessageBox.Show($"Invalid k value (Max = {calculatedMetricValues.Count} for this data and method)");
                        return;
                    }
                    else
                    {
                        int k = Int32.Parse(textBoxK.Text);
                        Dictionary<string, string> decision = ClassifyMethodFirst(k, calculatedMetricValues);
                        textBoxLog.Text += $"knn {decision.First().Key} the data ({decision.First().Value}).{Environment.NewLine}";
                        ScrollLogToBottom();
                    }
                }
                //METHOD 2 (TAKE k SMALLEST VALUES FROM EACH DECISION CLASS)
                else
                {
                    //TODO CHANGE CLASSIFICATION TO SYMBOL NOT bujf
                    //TODO METHOD 2 
                    //TODO PERCENTAGE OF CORRECT NORMALIZATION
                }
            }
        }

        //METHOD 1 (TAKE k SMALLEST VALUES FROM CALCULATED METRIC VALUES)
        Dictionary<string, string> ClassifyMethodFirst(int k, List<List<double>> calculatedMetricValues)
        {
            List<List<double>> sortedList = calculatedMetricValues.OrderBy(x => x[0]).ToList();
            List<List<double>> smallestValues = new List<List<double>>();
            for (int i = 0; i < k; i++)
            {
                smallestValues.Add(sortedList[i]);
            }
            Dictionary<string, string> decision = MakeDecision(smallestValues);
            return decision;
        }

        List<double> Manhattan(List<double> classifiedObject, List<double> objectToClassify)
        {
            List<double> result = new List<double>(2);

            double x = 0;
            for (int i = 0; i < objectToClassify.Count; i++)
            {
                x += Math.Abs(classifiedObject[i] - objectToClassify[i]);
            }
            result.Add(x);

            result.Add(classifiedObject.Last());
            return result;
        }

        List<double> Euclidean(List<double> classifiedObject, List<double> objectToClassify)
        {
            List<double> result = new List<double>(2);

            double x = 0;
            for (int i = 0; i < objectToClassify.Count; i++)
            {
                x += Math.Pow(classifiedObject[i] - objectToClassify[i], 2);
            }
            result.Add(Math.Sqrt(x));

            result.Add(classifiedObject.Last());
            return result;
        }

        List<double> Czybyszew(List<double> classifiedObject, List<double> objectToClassify)
        {
            List<double> result = new List<double>(2);

            double x = Math.Abs(classifiedObject.Max()-objectToClassify.Max());
            result.Add(x);

            result.Add(classifiedObject.Last());
            return result;
        }

        List<double> Minkowski(List<double> classifiedObject, List<double> objectToClassify)
        {
            List<double> result = new List<double>(2);
            int p = Int32.Parse(textBox1.Text);

            double x = 0;
            for (int i = 0; i < objectToClassify.Count; i++)
            {
                x += Math.Pow(Math.Abs(classifiedObject[i] - objectToClassify[i]), p);
            }
            result.Add(Math.Pow(x, (float)1/p));

            result.Add(classifiedObject.Last());
            return result;
        }

        List<double> Logarithm(List<double> classifiedObject, List<double> objectToClassify)
        {
            List<double> result = new List<double>(2);

            double x = 0;
            for (int i = 0; i < objectToClassify.Count; i++)
            {
                x += Math.Abs(Math.Log10(classifiedObject[i]) - Math.Log10(objectToClassify[i]));
            }
            result.Add(x);

            result.Add(classifiedObject.Last());
            return result;
        }

        //Works only if there is the same number of columns in each row
        List<List<double>> TransposeList(List<List<double>> a)
        {
            List<List<double>> result = new List<List<double>>();
            int columns = a.Count;
            int rows = a[0].Count;
            for (int i = 0; i < rows; i++)
            {
                List<double> tmp = new List<double>();
                for (int j = 0; j < columns; j++)
                {
                    tmp.Add(a[j][i]);
                }
                result.Add(tmp);
            }
            return result;
        }

        Dictionary<string, string> MakeDecision(List<List<double>> data)
        {
            Dictionary<string, int> dict = new Dictionary<string, int>();
            Dictionary<string, string> result = new Dictionary<string, string>();
            foreach (var x in data)
            {
                if (!dict.ContainsKey(x[1].ToString()))
                {
                    dict.Add(x[1].ToString(), 1);
                }
                else
                {
                    dict[x[1].ToString()] += 1;
                }
            }
            //sort dictionary
            dict = dict.OrderBy(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
            if (dict.Count>1 && dict.Last().Value == dict.SkipLast(1).Last().Value)
            {
                result.Add("can't classify", "?");
            }
            else
            {
                result.Add("classified", $"{dict.Last().Key}");
            }

            return result;
        }

    }
}
