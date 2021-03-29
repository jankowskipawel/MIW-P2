
namespace MIW_P2
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxDatasetPath = new System.Windows.Forms.TextBox();
            this.buttonFileDialog = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.buttonLoadData = new System.Windows.Forms.Button();
            this.textBoxDataToClassify = new System.Windows.Forms.TextBox();
            this.textBoxK = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxDatasetDelimiter = new System.Windows.Forms.TextBox();
            this.textBoxLog = new System.Windows.Forms.TextBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.buttonClassify = new System.Windows.Forms.Button();
            this.buttonClearData = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Dataset path:";
            // 
            // textBoxDatasetPath
            // 
            this.textBoxDatasetPath.Location = new System.Drawing.Point(12, 31);
            this.textBoxDatasetPath.Name = "textBoxDatasetPath";
            this.textBoxDatasetPath.Size = new System.Drawing.Size(395, 23);
            this.textBoxDatasetPath.TabIndex = 1;
            // 
            // buttonFileDialog
            // 
            this.buttonFileDialog.Location = new System.Drawing.Point(413, 31);
            this.buttonFileDialog.Name = "buttonFileDialog";
            this.buttonFileDialog.Size = new System.Drawing.Size(25, 23);
            this.buttonFileDialog.TabIndex = 2;
            this.buttonFileDialog.Text = "...";
            this.buttonFileDialog.UseVisualStyleBackColor = true;
            this.buttonFileDialog.Click += new System.EventHandler(this.button1_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // buttonLoadData
            // 
            this.buttonLoadData.Location = new System.Drawing.Point(158, 61);
            this.buttonLoadData.Name = "buttonLoadData";
            this.buttonLoadData.Size = new System.Drawing.Size(121, 23);
            this.buttonLoadData.TabIndex = 3;
            this.buttonLoadData.Text = "Load data";
            this.buttonLoadData.UseVisualStyleBackColor = true;
            this.buttonLoadData.Click += new System.EventHandler(this.buttonLoadData_Click);
            // 
            // textBoxDataToClassify
            // 
            this.textBoxDataToClassify.Location = new System.Drawing.Point(13, 112);
            this.textBoxDataToClassify.Name = "textBoxDataToClassify";
            this.textBoxDataToClassify.PlaceholderText = "Values separated by space (ex. 1 2 3 4 5 ...)";
            this.textBoxDataToClassify.Size = new System.Drawing.Size(425, 23);
            this.textBoxDataToClassify.TabIndex = 4;
            // 
            // textBoxK
            // 
            this.textBoxK.Location = new System.Drawing.Point(211, 161);
            this.textBoxK.Name = "textBoxK";
            this.textBoxK.Size = new System.Drawing.Size(106, 23);
            this.textBoxK.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 94);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 15);
            this.label2.TabIndex = 6;
            this.label2.Text = "Data to classify:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(211, 143);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(16, 15);
            this.label3.TabIndex = 7;
            this.label3.Text = "k:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 65);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 15);
            this.label4.TabIndex = 8;
            this.label4.Text = "Delimiter:";
            // 
            // textBoxDatasetDelimiter
            // 
            this.textBoxDatasetDelimiter.Location = new System.Drawing.Point(77, 62);
            this.textBoxDatasetDelimiter.Name = "textBoxDatasetDelimiter";
            this.textBoxDatasetDelimiter.Size = new System.Drawing.Size(39, 23);
            this.textBoxDatasetDelimiter.TabIndex = 9;
            this.textBoxDatasetDelimiter.Text = ";";
            // 
            // textBoxLog
            // 
            this.textBoxLog.Location = new System.Drawing.Point(13, 194);
            this.textBoxLog.Multiline = true;
            this.textBoxLog.Name = "textBoxLog";
            this.textBoxLog.ReadOnly = true;
            this.textBoxLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxLog.Size = new System.Drawing.Size(425, 244);
            this.textBoxLog.TabIndex = 10;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Manhattan",
            "Euclidean",
            "Czybyszew",
            "Minkowski",
            "Logarithm"});
            this.comboBox1.Location = new System.Drawing.Point(13, 161);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(192, 23);
            this.comboBox1.TabIndex = 11;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 143);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(44, 15);
            this.label5.TabIndex = 12;
            this.label5.Text = "Metric:";
            // 
            // buttonClassify
            // 
            this.buttonClassify.Enabled = false;
            this.buttonClassify.Location = new System.Drawing.Point(323, 161);
            this.buttonClassify.Name = "buttonClassify";
            this.buttonClassify.Size = new System.Drawing.Size(114, 23);
            this.buttonClassify.TabIndex = 13;
            this.buttonClassify.Text = "Classify";
            this.buttonClassify.UseVisualStyleBackColor = true;
            this.buttonClassify.Click += new System.EventHandler(this.buttonClassify_Click);
            // 
            // buttonClearData
            // 
            this.buttonClearData.Enabled = false;
            this.buttonClearData.Location = new System.Drawing.Point(285, 61);
            this.buttonClearData.Name = "buttonClearData";
            this.buttonClearData.Size = new System.Drawing.Size(122, 23);
            this.buttonClearData.TabIndex = 14;
            this.buttonClearData.Text = "Clear data";
            this.buttonClearData.UseVisualStyleBackColor = true;
            this.buttonClearData.Click += new System.EventHandler(this.buttonClearData_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(449, 450);
            this.Controls.Add(this.buttonClearData);
            this.Controls.Add(this.buttonClassify);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.textBoxLog);
            this.Controls.Add(this.textBoxDatasetDelimiter);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxK);
            this.Controls.Add(this.textBoxDataToClassify);
            this.Controls.Add(this.buttonLoadData);
            this.Controls.Add(this.buttonFileDialog);
            this.Controls.Add(this.textBoxDatasetPath);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(465, 489);
            this.MinimumSize = new System.Drawing.Size(465, 489);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxDatasetPath;
        private System.Windows.Forms.Button buttonFileDialog;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button buttonLoadData;
        private System.Windows.Forms.TextBox textBoxDataToClassify;
        private System.Windows.Forms.TextBox textBoxK;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxDatasetDelimiter;
        private System.Windows.Forms.TextBox textBoxLog;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button buttonClassify;
        private System.Windows.Forms.Button buttonClearData;
    }
}

