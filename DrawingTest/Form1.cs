using DrawingTest.CLasses;
using ImageProcessingMM.EngineClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace DrawingTest
{
    public partial class Form1 : Form
    {

        int x = 1;
        int y = 1;
        public Series ser1;
        public Series ser2;
        public Series ser3;


        public Series seriesHExt;
        public Series seriesSExt;
        public Series seriesVExt;

        string filePathInner { get; set; }
        public DirectBitmap directBitmap { get; set; }

        public HistogramData histogramDataRGBMain;

        public HistogramData histogramDataRGBMainProp
        {
            get
            {
                return histogramDataRGBMain;
            }
            set
            {
                histogramDataRGBMain = value;
                boi(histogramDataRGBMain,RGBMain);
            }
        }


        public HistogramData histogramDataLineRGB { get; set; }

        public HistogramData histogramDataLineRGBProp
        {
            get
            {
                return histogramDataLineRGB;
            }
            set
            {
                histogramDataLineRGB = value;
                boi(histogramDataLineRGB, RGBLine);
            }
        }

        public HistogramData histogramDataHSV { get; set; }
        public HistogramData histogramDataHSVProp
        {
            get
            {
                return histogramDataHSV;
            }
            set
            {
                histogramDataHSV = value;
                updateHSV_H(histogramDataHSV, HSV_H);
                updateHSV_H(histogramDataHSV, HSV_SV);
            }
        }


        //  = Point.Empty;//Point.Empty represents null for a Point object

        bool isMouseDown = new Boolean();//this is used to evaluate whether our mousebutton is down or not

        List<DrawnPoint> drawnPointList;



        public Form1()

        {
            //605 355
           
            InitializeComponent();

            ser1 = new Series
            {
                Name = "seriesRed",
                Color = System.Drawing.Color.Red,
                IsVisibleInLegend = true,
                IsXValueIndexed = true,
                ChartType = SeriesChartType.Column

            };
            ser1.IsXValueIndexed = false;

            ser2 = new Series
            {
                Name = "seriesGreen",
                Color = System.Drawing.Color.Green,
                IsVisibleInLegend = true,
                IsXValueIndexed = true,
                ChartType = SeriesChartType.Column
            };

            ser2.IsXValueIndexed = false;
            ser3 = new Series
            {
                Name = "seriesBlue",
                Color = System.Drawing.Color.Blue,
                IsVisibleInLegend = true,
                IsXValueIndexed = true,
                ChartType = SeriesChartType.Column
            };

            ser3.IsXValueIndexed = false;

            RGBLine.Series.Add(ser1);
            RGBLine.Series.Add(ser2);
            RGBLine.Series.Add(ser3);



            seriesHExt = new Series
            {
                Name = "seriesH",
                Color = Color.Aqua,
                IsVisibleInLegend = false,
                IsXValueIndexed = false,
                ChartType = SeriesChartType.Point
            };

            seriesSExt = new Series
            {
                Name = "seriesS",
                Color = Color.Black,
                IsVisibleInLegend = true,
                IsXValueIndexed = false,
                ChartType = SeriesChartType.Point
            };

            seriesVExt = new Series
            {
                Name = "seriesV",
                Color = Color.Red,
                IsVisibleInLegend = true,
                IsXValueIndexed = false,
                ChartType = SeriesChartType.Point
            };

            HLine.Series.Add(seriesHExt);

            SVLine.Series.Add(seriesSExt);
            SVLine.Series.Add(seriesVExt);

        }

     
        private void LoadFile_Click(object sender, EventArgs e)
        {
            //isGrey = false;
            var fileContent = string.Empty;
            var filePath = string.Empty;
            Image image;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "BMP files (*.bmp)|*.bmp|PNG files (*.png)|*.png|JPG files (*.jpg)|*.jpg|JPEG files (*.jpeg)|*.jpeg|GIF files (*.gif)|*.gif";
                openFileDialog.FilterIndex = 0;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path
                    filePath = openFileDialog.FileName;
                    var fileStream = openFileDialog.OpenFile();
                    //pictureBox1.ImageLocation = filePath;
                    //Dilation added using emguCV
                    //imageEmgu = new Image<Bgr, byte>(filePath);
                    filePathInner = filePath;
                    Image img = Image.FromFile(filePathInner);
                    //pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;

                    Form2 form2 = new Form2(this);
                    form2.ImageToShow = img;
                    

                    form2.Show();
                    //form2.Dispose();
                   // imageProcessingEngine = new ImageProcessingEngine(fileStream);
                    //imageProcessingEngine.generateHistogram();
                    //ImagePostBox.Image = null;

                    //  ImageBox.Paint += 
                }

            }
        }


        void initializeLine()
        {

        }


        void updateHSV_H(HistogramData histogramData, Chart chartControl)
        {
            chartControl.Series.Clear();


            if (chartControl.Name == "HSV_H")
            {
                var seriesH = new Series
                {
                    Name = "seriesHUE",
                    Color = Color.Aqua,
                    IsVisibleInLegend = false,
                    IsXValueIndexed = false,
                    ChartType = SeriesChartType.FastPoint
                };
                seriesH.XValueType = ChartValueType.Double;
                chartControl.Series.Add(seriesH);

                foreach (var a in histogramData.HSV_HHisto)
                {
                    seriesH.Points.AddXY(a.Key, a.Value);
                }
            }

            if(chartControl.Name == "HSV_SV")
            {
                var seriesS = new Series
                {
                    Name = "seriesSaturation",
                    Color = Color.Black,
                    IsVisibleInLegend = true,
                    IsXValueIndexed = false,
                    ChartType = SeriesChartType.FastPoint

                };

                seriesS.XValueType = ChartValueType.Double;

                var seriesV = new Series
                {
                    Name = "seriesValue",
                    Color = Color.Red,
                    IsVisibleInLegend = true,
                    IsXValueIndexed = false,
                    ChartType = SeriesChartType.FastPoint
                };

                seriesV.XValueType = ChartValueType.Double;

                chartControl.Series.Add(seriesS);
                chartControl.Series.Add(seriesV);

                foreach (var a in histogramData.HSV_SHisto)
                {
                    seriesS.Points.AddXY(a.Key, a.Value);
                }

                foreach (var a in histogramData.HSV_VHisto)
                {
                    seriesV.Points.AddXY(a.Key, a.Value);
                }


            }
           /// ChartAreaCollection col = chartControl.ChartAreas;

        }


        void boi(HistogramData histogramData, Chart chartControl)
        {
            //HistogramData histogramData = imageProcessingEngine.hitogramsData;


            chartControl.Series.Clear();

            var seriesRed = new System.Windows.Forms.DataVisualization.Charting.Series
            {
                Name = "seriesRed",
                Color = System.Drawing.Color.Red,
                IsVisibleInLegend = true,
                IsXValueIndexed = true,
                ChartType = SeriesChartType.Column
            };

            var seriesGreen = new Series
            {
                Name = "seriesGreen",
                Color = System.Drawing.Color.Green,
                IsVisibleInLegend = true,
                IsXValueIndexed = true,
                ChartType = SeriesChartType.Column
            };

            var seriesBlue = new Series
            {
                Name = "seriesBlue",
                Color = System.Drawing.Color.Blue,
                IsVisibleInLegend = true,
                IsXValueIndexed = true,
                ChartType = SeriesChartType.Column
            };

            chartControl.Series.Add(seriesRed);

            chartControl.Series.Add(seriesGreen);
            chartControl.Series.Add(seriesBlue);
         

            for (int i = 0; i < 256; i++)
            {
                seriesRed.Points.AddXY(i, histogramData.redHisto[i]);
                seriesGreen.Points.AddXY(i, histogramData.greenHisto[i]);
                seriesBlue.Points.AddXY(i, histogramData.blueHisto[i]);
                
            }


            //ChartGroup.Visible = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int[] maks = { 1, 2, 3, 4,5, 6 };
           // Form3 form3 = new Form3(3,2,maks );
           // form3.ImageToShow = img;


           // form3.Show();
        }
    }
}
