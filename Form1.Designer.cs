
namespace CombinationsTool
{
    partial class MainForm
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
            this.lbValues = new System.Windows.Forms.Label();
            this.lbCategories = new System.Windows.Forms.Label();
            this.lbFinalSum = new System.Windows.Forms.Label();
            this.tbValues = new System.Windows.Forms.TextBox();
            this.tbCategories = new System.Windows.Forms.TextBox();
            this.tbFinalSum = new System.Windows.Forms.TextBox();
            this.btCalculate = new System.Windows.Forms.Button();
            this.lbResult = new System.Windows.Forms.ListBox();
            this.lbResultCount = new System.Windows.Forms.Label();
            this.btClean = new System.Windows.Forms.Button();
            this.lbTime = new System.Windows.Forms.Label();
            this.lbCombinationsTime = new System.Windows.Forms.Label();
            this.lbWritingTime = new System.Windows.Forms.Label();
            this.lbThreads = new System.Windows.Forms.Label();
            this.lbAllCombinations = new System.Windows.Forms.Label();
            this.cbThreads = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // lbValues
            // 
            this.lbValues.AutoSize = true;
            this.lbValues.Location = new System.Drawing.Point(12, 9);
            this.lbValues.Name = "lbValues";
            this.lbValues.Size = new System.Drawing.Size(169, 15);
            this.lbValues.TabIndex = 0;
            this.lbValues.Text = "Informe o conjunto de valores:";
            // 
            // lbCategories
            // 
            this.lbCategories.AutoSize = true;
            this.lbCategories.Location = new System.Drawing.Point(12, 53);
            this.lbCategories.Name = "lbCategories";
            this.lbCategories.Size = new System.Drawing.Size(254, 15);
            this.lbCategories.TabIndex = 1;
            this.lbCategories.Text = "Informe a categoria a qual cada valor pertence:";
            // 
            // lbFinalSum
            // 
            this.lbFinalSum.AutoSize = true;
            this.lbFinalSum.Location = new System.Drawing.Point(804, 9);
            this.lbFinalSum.Name = "lbFinalSum";
            this.lbFinalSum.Size = new System.Drawing.Size(118, 15);
            this.lbFinalSum.TabIndex = 2;
            this.lbFinalSum.Text = "Informe o valor total:";
            // 
            // tbValues
            // 
            this.tbValues.Location = new System.Drawing.Point(12, 27);
            this.tbValues.Name = "tbValues";
            this.tbValues.Size = new System.Drawing.Size(786, 23);
            this.tbValues.TabIndex = 3;
            // 
            // tbCategories
            // 
            this.tbCategories.Location = new System.Drawing.Point(12, 71);
            this.tbCategories.Name = "tbCategories";
            this.tbCategories.Size = new System.Drawing.Size(786, 23);
            this.tbCategories.TabIndex = 4;
            // 
            // tbFinalSum
            // 
            this.tbFinalSum.Location = new System.Drawing.Point(804, 27);
            this.tbFinalSum.Name = "tbFinalSum";
            this.tbFinalSum.Size = new System.Drawing.Size(183, 23);
            this.tbFinalSum.TabIndex = 5;
            // 
            // btCalculate
            // 
            this.btCalculate.Location = new System.Drawing.Point(899, 100);
            this.btCalculate.Name = "btCalculate";
            this.btCalculate.Size = new System.Drawing.Size(88, 24);
            this.btCalculate.TabIndex = 6;
            this.btCalculate.Text = "Calcular";
            this.btCalculate.UseVisualStyleBackColor = true;
            this.btCalculate.Click += new System.EventHandler(this.btCalculate_Click);
            // 
            // lbResult
            // 
            this.lbResult.FormattingEnabled = true;
            this.lbResult.ItemHeight = 15;
            this.lbResult.Location = new System.Drawing.Point(12, 131);
            this.lbResult.Name = "lbResult";
            this.lbResult.Size = new System.Drawing.Size(975, 319);
            this.lbResult.TabIndex = 7;
            // 
            // lbResultCount
            // 
            this.lbResultCount.AutoSize = true;
            this.lbResultCount.Location = new System.Drawing.Point(956, 453);
            this.lbResultCount.Name = "lbResultCount";
            this.lbResultCount.Size = new System.Drawing.Size(31, 15);
            this.lbResultCount.TabIndex = 9;
            this.lbResultCount.Text = "0000";
            // 
            // btClean
            // 
            this.btClean.Location = new System.Drawing.Point(804, 100);
            this.btClean.Name = "btClean";
            this.btClean.Size = new System.Drawing.Size(89, 24);
            this.btClean.TabIndex = 10;
            this.btClean.Text = "Apagar";
            this.btClean.UseVisualStyleBackColor = true;
            this.btClean.Click += new System.EventHandler(this.btClean_Click);
            // 
            // lbTime
            // 
            this.lbTime.AutoSize = true;
            this.lbTime.Location = new System.Drawing.Point(199, 453);
            this.lbTime.Name = "lbTime";
            this.lbTime.Size = new System.Drawing.Size(76, 15);
            this.lbTime.TabIndex = 11;
            this.lbTime.Text = "T: 00:00:00:00";
            // 
            // lbCombinationsTime
            // 
            this.lbCombinationsTime.AutoSize = true;
            this.lbCombinationsTime.Location = new System.Drawing.Point(12, 453);
            this.lbCombinationsTime.Name = "lbCombinationsTime";
            this.lbCombinationsTime.Size = new System.Drawing.Size(78, 15);
            this.lbCombinationsTime.TabIndex = 13;
            this.lbCombinationsTime.Text = "C: 00:00:00:00";
            // 
            // lbWritingTime
            // 
            this.lbWritingTime.AutoSize = true;
            this.lbWritingTime.Location = new System.Drawing.Point(105, 453);
            this.lbWritingTime.Name = "lbWritingTime";
            this.lbWritingTime.Size = new System.Drawing.Size(76, 15);
            this.lbWritingTime.TabIndex = 15;
            this.lbWritingTime.Text = "E: 00:00:00:00";
            // 
            // lbThreads
            // 
            this.lbThreads.AutoSize = true;
            this.lbThreads.Location = new System.Drawing.Point(804, 53);
            this.lbThreads.Name = "lbThreads";
            this.lbThreads.Size = new System.Drawing.Size(130, 15);
            this.lbThreads.TabIndex = 16;
            this.lbThreads.Text = "Quantidade de threads:";
            // 
            // lbAllCombinations
            // 
            this.lbAllCombinations.AutoSize = true;
            this.lbAllCombinations.Location = new System.Drawing.Point(12, 106);
            this.lbAllCombinations.Name = "lbAllCombinations";
            this.lbAllCombinations.Size = new System.Drawing.Size(79, 15);
            this.lbAllCombinations.TabIndex = 18;
            this.lbAllCombinations.Text = "000000000000";
            // 
            // cbThreads
            // 
            this.cbThreads.FormattingEnabled = true;
            this.cbThreads.Location = new System.Drawing.Point(804, 71);
            this.cbThreads.Name = "cbThreads";
            this.cbThreads.Size = new System.Drawing.Size(183, 23);
            this.cbThreads.TabIndex = 19;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(999, 473);
            this.Controls.Add(this.cbThreads);
            this.Controls.Add(this.lbAllCombinations);
            this.Controls.Add(this.lbThreads);
            this.Controls.Add(this.lbWritingTime);
            this.Controls.Add(this.lbCombinationsTime);
            this.Controls.Add(this.lbTime);
            this.Controls.Add(this.btClean);
            this.Controls.Add(this.lbResultCount);
            this.Controls.Add(this.lbResult);
            this.Controls.Add(this.btCalculate);
            this.Controls.Add(this.tbFinalSum);
            this.Controls.Add(this.tbCategories);
            this.Controls.Add(this.tbValues);
            this.Controls.Add(this.lbFinalSum);
            this.Controls.Add(this.lbCategories);
            this.Controls.Add(this.lbValues);
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "Combinations Tool by LWMolin (v. 2021.07.10.1)";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbValues;
        private System.Windows.Forms.Label lbCategories;
        private System.Windows.Forms.Label lbFinalSum;
        private System.Windows.Forms.TextBox tbValues;
        private System.Windows.Forms.TextBox tbCategories;
        private System.Windows.Forms.TextBox tbFinalSum;
        private System.Windows.Forms.Button btCalculate;
        private System.Windows.Forms.ListBox lbResult;
        private System.Windows.Forms.Label lbResultCount;
        private System.Windows.Forms.Button btClean;
        private System.Windows.Forms.Label lbTime;
        private System.Windows.Forms.Label lbCombinationsTime;
        private System.Windows.Forms.Label lbWritingTime;
        private System.Windows.Forms.Label lbThreads;
        private System.Windows.Forms.Label lbAllCombinations;
        private System.Windows.Forms.ComboBox cbThreads;
    }
}

