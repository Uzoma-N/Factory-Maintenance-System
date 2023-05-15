namespace Maintenance_system
{
    partial class User_Interface
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.treeView = new System.Windows.Forms.TreeView();
            this.nodeDetails = new System.Windows.Forms.TextBox();
            this.useFormular = new System.Windows.Forms.Button();
            this.predictBox = new System.Windows.Forms.TextBox();
            this.applyANN = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.textWear = new System.Windows.Forms.TextBox();
            this.textQuaT = new System.Windows.Forms.TextBox();
            this.textEff = new System.Windows.Forms.TextBox();
            this.labWRate = new System.Windows.Forms.Label();
            this.labEff = new System.Windows.Forms.Label();
            this.labQual = new System.Windows.Forms.Label();
            this.Maintenance = new System.Windows.Forms.Label();
            this.minBox = new System.Windows.Forms.TextBox();
            this.maxBox = new System.Windows.Forms.TextBox();
            this.Maximum = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.minLabel = new System.Windows.Forms.Label();
            this.btnInconCheck = new System.Windows.Forms.Button();
            this.chartValues = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.btnStart = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.chartValues)).BeginInit();
            this.SuspendLayout();
            // 
            // treeView
            // 
            this.treeView.Location = new System.Drawing.Point(17, 16);
            this.treeView.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.treeView.Name = "treeView";
            this.treeView.Size = new System.Drawing.Size(289, 254);
            this.treeView.TabIndex = 0;
            this.treeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView_AfterSelect);
            // 
            // nodeDetails
            // 
            this.nodeDetails.Location = new System.Drawing.Point(316, 17);
            this.nodeDetails.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.nodeDetails.Multiline = true;
            this.nodeDetails.Name = "nodeDetails";
            this.nodeDetails.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.nodeDetails.Size = new System.Drawing.Size(380, 251);
            this.nodeDetails.TabIndex = 1;
            this.nodeDetails.TextChanged += new System.EventHandler(this.nodeDetails_TextChanged);
            // 
            // useFormular
            // 
            this.useFormular.Location = new System.Drawing.Point(721, 53);
            this.useFormular.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.useFormular.Name = "useFormular";
            this.useFormular.Size = new System.Drawing.Size(100, 28);
            this.useFormular.TabIndex = 5;
            this.useFormular.Text = "Get Input";
            this.useFormular.UseVisualStyleBackColor = true;
            this.useFormular.Click += new System.EventHandler(this.useFormular_Click);
            // 
            // predictBox
            // 
            this.predictBox.Location = new System.Drawing.Point(721, 293);
            this.predictBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.predictBox.Multiline = true;
            this.predictBox.Name = "predictBox";
            this.predictBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.predictBox.Size = new System.Drawing.Size(460, 179);
            this.predictBox.TabIndex = 6;
            // 
            // applyANN
            // 
            this.applyANN.Location = new System.Drawing.Point(724, 201);
            this.applyANN.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.applyANN.Name = "applyANN";
            this.applyANN.Size = new System.Drawing.Size(100, 28);
            this.applyANN.TabIndex = 8;
            this.applyANN.Text = "Tell State";
            this.applyANN.UseVisualStyleBackColor = true;
            this.applyANN.Click += new System.EventHandler(this.applyANN_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(1085, 505);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(100, 28);
            this.btnDelete.TabIndex = 8;
            this.btnDelete.Text = "Clear";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // textWear
            // 
            this.textWear.Location = new System.Drawing.Point(723, 138);
            this.textWear.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textWear.Name = "textWear";
            this.textWear.Size = new System.Drawing.Size(132, 22);
            this.textWear.TabIndex = 9;
            // 
            // textQuaT
            // 
            this.textQuaT.Location = new System.Drawing.Point(1061, 137);
            this.textQuaT.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textQuaT.Name = "textQuaT";
            this.textQuaT.Size = new System.Drawing.Size(132, 22);
            this.textQuaT.TabIndex = 9;
            // 
            // textEff
            // 
            this.textEff.Location = new System.Drawing.Point(892, 137);
            this.textEff.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textEff.Name = "textEff";
            this.textEff.Size = new System.Drawing.Size(132, 22);
            this.textEff.TabIndex = 9;
            // 
            // labWRate
            // 
            this.labWRate.AutoSize = true;
            this.labWRate.Location = new System.Drawing.Point(719, 117);
            this.labWRate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labWRate.Name = "labWRate";
            this.labWRate.Size = new System.Drawing.Size(76, 17);
            this.labWRate.TabIndex = 10;
            this.labWRate.Text = "Wear Rate";
            // 
            // labEff
            // 
            this.labEff.AutoSize = true;
            this.labEff.Location = new System.Drawing.Point(888, 117);
            this.labEff.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labEff.Name = "labEff";
            this.labEff.Size = new System.Drawing.Size(68, 17);
            this.labEff.TabIndex = 10;
            this.labEff.Text = "Efficiency";
            // 
            // labQual
            // 
            this.labQual.AutoSize = true;
            this.labQual.Location = new System.Drawing.Point(1057, 117);
            this.labQual.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labQual.Name = "labQual";
            this.labQual.Size = new System.Drawing.Size(92, 17);
            this.labQual.TabIndex = 10;
            this.labQual.Text = "Quality Track";
            // 
            // Maintenance
            // 
            this.Maintenance.AutoSize = true;
            this.Maintenance.Location = new System.Drawing.Point(951, 201);
            this.Maintenance.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Maintenance.Name = "Maintenance";
            this.Maintenance.Size = new System.Drawing.Size(135, 17);
            this.Maintenance.TabIndex = 11;
            this.Maintenance.Text = "Maintenance Range";
            // 
            // minBox
            // 
            this.minBox.Location = new System.Drawing.Point(955, 249);
            this.minBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.minBox.Name = "minBox";
            this.minBox.Size = new System.Drawing.Size(68, 22);
            this.minBox.TabIndex = 12;
            // 
            // maxBox
            // 
            this.maxBox.Location = new System.Drawing.Point(1056, 250);
            this.maxBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.maxBox.Name = "maxBox";
            this.maxBox.Size = new System.Drawing.Size(68, 22);
            this.maxBox.TabIndex = 12;
            // 
            // Maximum
            // 
            this.Maximum.AutoSize = true;
            this.Maximum.Location = new System.Drawing.Point(1089, 229);
            this.Maximum.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Maximum.Name = "Maximum";
            this.Maximum.Size = new System.Drawing.Size(33, 17);
            this.Maximum.TabIndex = 14;
            this.Maximum.Text = "Max";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1035, 254);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(13, 17);
            this.label1.TabIndex = 15;
            this.label1.Text = "-";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(1128, 258);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 17);
            this.label2.TabIndex = 16;
            this.label2.Text = "months";
            // 
            // minLabel
            // 
            this.minLabel.AutoSize = true;
            this.minLabel.Location = new System.Drawing.Point(951, 229);
            this.minLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.minLabel.Name = "minLabel";
            this.minLabel.Size = new System.Drawing.Size(30, 17);
            this.minLabel.TabIndex = 14;
            this.minLabel.Text = "Min";
            // 
            // btnInconCheck
            // 
            this.btnInconCheck.Location = new System.Drawing.Point(980, 17);
            this.btnInconCheck.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnInconCheck.Name = "btnInconCheck";
            this.btnInconCheck.Size = new System.Drawing.Size(189, 28);
            this.btnInconCheck.TabIndex = 3;
            this.btnInconCheck.Text = "Inconsistency Check";
            this.btnInconCheck.UseVisualStyleBackColor = true;
            this.btnInconCheck.Click += new System.EventHandler(this.btnInconCheck_Click);
            // 
            // chartValues
            // 
            chartArea1.Name = "ChartArea1";
            this.chartValues.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chartValues.Legends.Add(legend1);
            this.chartValues.Location = new System.Drawing.Point(17, 305);
            this.chartValues.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chartValues.Name = "chartValues";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.Legend = "Legend1";
            series1.Name = "valuesPlot";
            this.chartValues.Series.Add(series1);
            this.chartValues.Size = new System.Drawing.Size(680, 228);
            this.chartValues.TabIndex = 22;
            this.chartValues.Text = "datavalues";
            this.chartValues.Click += new System.EventHandler(this.chartValues_Click);
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(1069, 53);
            this.btnStart.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(100, 28);
            this.btnStart.TabIndex = 27;
            this.btnStart.Text = "Server";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click_1);
            // 
            // User_Interface
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(1201, 548);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.chartValues);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.minLabel);
            this.Controls.Add(this.Maximum);
            this.Controls.Add(this.maxBox);
            this.Controls.Add(this.minBox);
            this.Controls.Add(this.Maintenance);
            this.Controls.Add(this.labQual);
            this.Controls.Add(this.labEff);
            this.Controls.Add(this.labWRate);
            this.Controls.Add(this.textEff);
            this.Controls.Add(this.textQuaT);
            this.Controls.Add(this.textWear);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.applyANN);
            this.Controls.Add(this.predictBox);
            this.Controls.Add(this.useFormular);
            this.Controls.Add(this.btnInconCheck);
            this.Controls.Add(this.nodeDetails);
            this.Controls.Add(this.treeView);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "User_Interface";
            this.Tag = "Predictive Maintenance System";
            this.Text = "User Interface";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chartValues)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView treeView;
        private System.Windows.Forms.TextBox nodeDetails;
        private System.Windows.Forms.Button useFormular;
        private System.Windows.Forms.TextBox predictBox;
        private System.Windows.Forms.Button applyANN;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.TextBox textWear;
        private System.Windows.Forms.TextBox textQuaT;
        private System.Windows.Forms.TextBox textEff;
        private System.Windows.Forms.Label labWRate;
        private System.Windows.Forms.Label labEff;
        private System.Windows.Forms.Label labQual;
        private System.Windows.Forms.Label Maintenance;
        private System.Windows.Forms.TextBox minBox;
        private System.Windows.Forms.TextBox maxBox;
        private System.Windows.Forms.Label Maximum;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label minLabel;
        private System.Windows.Forms.Button btnInconCheck;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartValues;
        private System.Windows.Forms.Button btnStart;
    }
}

