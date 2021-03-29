using System;
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
                if (dataset.attributes.Count == 0)
                {
                    textBoxLog.Text += $"WARNING: dataset is empty!{Environment.NewLine}";
                }
                textBoxLog.Text += $"Dataset loaded.{Environment.NewLine}";
                buttonLoadData.Enabled = false;
                buttonLoadData.Text = "Dataset loaded";
                buttonClassify.Enabled = true;
                buttonClearData.Enabled = true;
                NormalizeData();
                ScrollLogToBottom();
            }
        }

        private void buttonClearData_Click(object sender, EventArgs e)
        {
            dataset = new Dataset();
            buttonLoadData.Enabled = true;
            buttonLoadData.Text = "Load data";
            buttonClearData.Enabled = false;
        }

        private void ScrollLogToBottom()
        {
            textBoxLog.SelectionStart = textBoxLog.Text.Length;
            textBoxLog.ScrollToCaret();
        }

        private void NormalizeData()
        {
            List<List<object>> normalizedAttributes = new List<List<object>>(dataset.attributes.Count);
            for (int i = 0; i < dataset.attributes.Count; i++)
            {
                if (dataset.attributeTypes[i] == "numeric")
                {
                    List<float> tmpList = new List<float>();
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
                    List<object> normalizedColumn = new List<object>(dataset.attributes[i].Count);
                    for (int j = 0; j < dataset.attributes[i].Count; j++)
                    {
                        double normalizedValue = ((double)tmpList[j] - minimum) / (maximum - minimum);
                        normalizedColumn.Add(normalizedValue);
                    }
                    normalizedAttributes.Add(normalizedColumn);
                }
                else
                {
                    List<string> uniqueStrings = CreateUniqueList(dataset.attributes[i]);
                    int length = uniqueStrings.Count;
                    List<object> normalizedColumn = new List<object>();
                    foreach (var x in dataset.attributes[i])
                    {
                        double tmp;
                        if (Convert.ToString(x) == "?")
                        {
                            tmp = 0;
                        }
                        else
                        {
                            tmp = uniqueStrings.IndexOf(Convert.ToString(x)) / ((double)length - 1);
                        }
                        normalizedColumn.Add(tmp);
                    }
                    normalizedAttributes.Add(normalizedColumn);
                }
            }

            dataset.attributes = normalizedAttributes;
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
    }
}
