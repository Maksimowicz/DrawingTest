namespace DrawingTest
{
    partial class Form1
    {
        /// <summary>
        /// Wymagana zmienna projektanta.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Wyczyść wszystkie używane zasoby.
        /// </summary>
        /// <param name="disposing">prawda, jeżeli zarządzane zasoby powinny zostać zlikwidowane; Fałsz w przeciwnym wypadku.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kod generowany przez Projektanta formularzy systemu Windows

        /// <summary>
        /// Metoda wymagana do obsługi projektanta — nie należy modyfikować
        /// jej zawartości w edytorze kodu.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea4 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend4 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea5 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend5 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series5 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea6 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend6 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series6 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.LoadFile = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.RGBMain = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.RGBLine = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.rgbStats = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.HSV_SV = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.HSV_H = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.SVLine = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.HLine = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RGBMain)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RGBLine)).BeginInit();
            this.rgbStats.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.HSV_SV)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.HSV_H)).BeginInit();
            this.tabPage3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SVLine)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.HLine)).BeginInit();
            this.SuspendLayout();
            // 
            // LoadFile
            // 
            this.LoadFile.Location = new System.Drawing.Point(12, 7);
            this.LoadFile.Name = "LoadFile";
            this.LoadFile.Size = new System.Drawing.Size(124, 34);
            this.LoadFile.TabIndex = 11;
            this.LoadFile.Text = "LoadFile";
            this.LoadFile.UseVisualStyleBackColor = true;
            this.LoadFile.Click += new System.EventHandler(this.LoadFile_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.RGBMain);
            this.groupBox1.Location = new System.Drawing.Point(21, 15);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1292, 341);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Image Stats";
            // 
            // RGBMain
            // 
            chartArea1.Name = "ChartArea1";
            this.RGBMain.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.RGBMain.Legends.Add(legend1);
            this.RGBMain.Location = new System.Drawing.Point(6, 21);
            this.RGBMain.Name = "RGBMain";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.RGBMain.Series.Add(series1);
            this.RGBMain.Size = new System.Drawing.Size(1280, 320);
            this.RGBMain.TabIndex = 0;
            this.RGBMain.Text = "chart1";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.RGBLine);
            this.groupBox2.Location = new System.Drawing.Point(27, 362);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1286, 336);
            this.groupBox2.TabIndex = 13;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Line profile";
            // 
            // RGBLine
            // 
            chartArea2.Name = "ChartArea1";
            this.RGBLine.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            this.RGBLine.Legends.Add(legend2);
            this.RGBLine.Location = new System.Drawing.Point(-6, 21);
            this.RGBLine.Name = "RGBLine";
            series2.ChartArea = "ChartArea1";
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            this.RGBLine.Series.Add(series2);
            this.RGBLine.Size = new System.Drawing.Size(1286, 314);
            this.RGBLine.TabIndex = 1;
            this.RGBLine.Text = "chart3";
            // 
            // rgbStats
            // 
            this.rgbStats.Controls.Add(this.tabPage1);
            this.rgbStats.Controls.Add(this.tabPage2);
            this.rgbStats.Controls.Add(this.tabPage3);
            this.rgbStats.Location = new System.Drawing.Point(12, 47);
            this.rgbStats.Name = "rgbStats";
            this.rgbStats.SelectedIndex = 0;
            this.rgbStats.Size = new System.Drawing.Size(1327, 744);
            this.rgbStats.TabIndex = 14;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1319, 715);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "RGB";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.groupBox3);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1319, 715);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "HSV Main";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.HSV_SV);
            this.groupBox3.Controls.Add(this.HSV_H);
            this.groupBox3.Location = new System.Drawing.Point(3, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(1310, 689);
            this.groupBox3.TabIndex = 13;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Image Stats";
            // 
            // HSV_SV
            // 
            chartArea3.Name = "ChartArea1";
            this.HSV_SV.ChartAreas.Add(chartArea3);
            legend3.Name = "Legend1";
            this.HSV_SV.Legends.Add(legend3);
            this.HSV_SV.Location = new System.Drawing.Point(-13, 327);
            this.HSV_SV.Name = "HSV_SV";
            series3.ChartArea = "ChartArea1";
            series3.Legend = "Legend1";
            series3.Name = "Series1";
            this.HSV_SV.Series.Add(series3);
            this.HSV_SV.Size = new System.Drawing.Size(1317, 340);
            this.HSV_SV.TabIndex = 1;
            this.HSV_SV.Text = "chart2";
            // 
            // HSV_H
            // 
            chartArea4.Name = "ChartArea1";
            this.HSV_H.ChartAreas.Add(chartArea4);
            legend4.Name = "Legend1";
            this.HSV_H.Legends.Add(legend4);
            this.HSV_H.Location = new System.Drawing.Point(6, 21);
            this.HSV_H.Name = "HSV_H";
            series4.ChartArea = "ChartArea1";
            series4.Legend = "Legend1";
            series4.Name = "Series1";
            this.HSV_H.Series.Add(series4);
            this.HSV_H.Size = new System.Drawing.Size(1304, 300);
            this.HSV_H.TabIndex = 0;
            this.HSV_H.Text = "chart1";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.groupBox4);
            this.tabPage3.Location = new System.Drawing.Point(4, 25);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(1319, 715);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "HSV Line";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.SVLine);
            this.groupBox4.Controls.Add(this.HLine);
            this.groupBox4.Location = new System.Drawing.Point(6, 6);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(1307, 703);
            this.groupBox4.TabIndex = 15;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Line profile";
            // 
            // SVLine
            // 
            chartArea5.Name = "ChartArea1";
            this.SVLine.ChartAreas.Add(chartArea5);
            legend5.Name = "Legend1";
            this.SVLine.Legends.Add(legend5);
            this.SVLine.Location = new System.Drawing.Point(6, 336);
            this.SVLine.Name = "SVLine";
            series5.ChartArea = "ChartArea1";
            series5.Legend = "Legend1";
            series5.Name = "Series1";
            this.SVLine.Series.Add(series5);
            this.SVLine.Size = new System.Drawing.Size(1307, 300);
            this.SVLine.TabIndex = 2;
            this.SVLine.Text = "chart4";
            // 
            // HLine
            // 
            chartArea6.Name = "ChartArea1";
            this.HLine.ChartAreas.Add(chartArea6);
            legend6.Name = "Legend1";
            this.HLine.Legends.Add(legend6);
            this.HLine.Location = new System.Drawing.Point(6, 21);
            this.HLine.Name = "HLine";
            series6.ChartArea = "ChartArea1";
            series6.Legend = "Legend1";
            series6.Name = "Series1";
            this.HLine.Series.Add(series6);
            this.HLine.Size = new System.Drawing.Size(1295, 300);
            this.HLine.TabIndex = 1;
            this.HLine.Text = "chart3";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1351, 803);
            this.Controls.Add(this.rgbStats);
            this.Controls.Add(this.LoadFile);
            this.Name = "Form1";
            this.Text = "Charts";
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.RGBMain)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.RGBLine)).EndInit();
            this.rgbStats.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.HSV_SV)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.HSV_H)).EndInit();
            this.tabPage3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SVLine)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.HLine)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button LoadFile;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataVisualization.Charting.Chart RGBMain;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataVisualization.Charting.Chart RGBLine;
        private System.Windows.Forms.TabControl rgbStats;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DataVisualization.Charting.Chart HSV_SV;
        private System.Windows.Forms.DataVisualization.Charting.Chart HSV_H;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.DataVisualization.Charting.Chart SVLine;
        private System.Windows.Forms.DataVisualization.Charting.Chart HLine;
    }
}

