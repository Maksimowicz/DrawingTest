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

        //Serie wykresów
        public Series ser1;
        public Series ser2;
        public Series ser3;

        //Series wykresów HSV
        public Series seriesHExt;
        public Series seriesSExt;
        public Series seriesVExt;

        //Ścieżka wczytanego pliku
        string filePathInner { get; set; }

        //Obiekt reprezentujący tablicę pikseli, tablica jednowymiarowa, rzutowana na 2 wymiarową
        public DirectBitmap directBitmap { get; set; }

        //Klasa reprezentujaća dane wykorzystywane na histogramie
        public HistogramData histogramDataRGBMain;

        //Właściwość klasy powyżej (pozwala na obsługę get; seet;)
        public HistogramData histogramDataRGBMainProp
        {
            get
            {
                return histogramDataRGBMain;
            }
            set
            {
                histogramDataRGBMain = value;
                updateCharts(histogramDataRGBMain,RGBMain);
            }
        }

        //Klasa reprezentująca histogram (dla profilu liniowego)
        public HistogramData histogramDataLineRGB { get; set; }

        //Właściwość dla pola powyżej
        public HistogramData histogramDataLineRGBProp
        {
            get
            {
                return histogramDataLineRGB;
            }
            set
            {
                histogramDataLineRGB = value;
                updateCharts(histogramDataLineRGB, RGBLine);
            }
        }

        //Klasa reprezentująca histogram (dla modelu HSV)
        public HistogramData histogramDataHSV { get; set; }
       //Właściwość pola powyżej
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

       
        //Lista punktów narysowanych na obrazie 
        List<DrawnPoint> drawnPointList;

        //Konstruktor formatki
        public Form1()
        {  
            InitializeComponent();

            //Inicjalizacja zbiorów danych dla wykresu RGB
            ser1 = new Series
            {
                Name = "seriesRed",
                Color = System.Drawing.Color.Red,
                IsVisibleInLegend = false,
                IsXValueIndexed = true,
                ChartType = SeriesChartType.Column,
            

            };
            ser1.IsXValueIndexed = false;
            
            ser2 = new Series
            {
                Name = "seriesGreen",
                Color = System.Drawing.Color.Green,
                IsVisibleInLegend = false,
                IsXValueIndexed = true,
                ChartType = SeriesChartType.Column
            };

            ser2.IsXValueIndexed = false;
            ser3 = new Series
            {
                Name = "seriesBlue",
                Color = System.Drawing.Color.Blue,
                IsVisibleInLegend = false,
                IsXValueIndexed = true,
                ChartType = SeriesChartType.Column
            };

            ser3.IsXValueIndexed = false;


            //Dodanie utworzonych zbiorów danych do konkretnego wykresu
            RGBLine.Series.Add(ser1);
            RGBLine.Series.Add(ser2);
            RGBLine.Series.Add(ser3);


            //Generowanie zbirów danych dla wykresów HSV
            seriesHExt = new Series
            {
                Name = "seriesH",
                Color = Color.Aqua,
                IsVisibleInLegend = false,
                IsXValueIndexed = false,
                ChartType = SeriesChartType.Column
            };

            seriesSExt = new Series
            {
                Name = "seriesS",
                Color = Color.Black,
                IsVisibleInLegend = false,
                IsXValueIndexed = false,
                ChartType = SeriesChartType.Column
            };

            seriesVExt = new Series
            {
                Name = "seriesV",
                Color = Color.Red,
                IsVisibleInLegend = false,
                IsXValueIndexed = false,
                ChartType = SeriesChartType.Column
            };

            //Przypisanie konkretnych źródeł do wykresów
            HLine.Series.Add(seriesHExt);

            SVLine.Series.Add(seriesSExt);
            SVLine.Series.Add(seriesVExt);

        }

     
        //Metoda ładująca plik do Form2
        private void LoadFile_Click(object sender, EventArgs e)
        {
           //Zmienne pomocnicze
            var fileContent = string.Empty;
            var filePath = string.Empty;
            Image image;

            //Rozpoczęcie procesu otwierania pliku
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                //zdefiniowanie możliwych rozszerzeń
                openFileDialog.Filter = "BMP files (*.bmp)|*.bmp|PNG files (*.png)|*.png|JPG files (*.jpg)|*.jpg|JPEG files (*.jpeg)|*.jpeg|GIF files (*.gif)|*.gif";
                openFileDialog.FilterIndex = 0;
                openFileDialog.RestoreDirectory = true;

                //jeżeli zatwierdzono dialog box
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //pobież ścieżkę
                    filePath = openFileDialog.FileName;
                    var fileStream = openFileDialog.OpenFile();
                
                    filePathInner = filePath;
                    Image img = Image.FromFile(filePathInner);
         
                    //inicjalizacja Form 2 (do obrabiania obrazu)
                    Form2 form2 = new Form2(this);
                    form2.ImageToShow = img;
                    

                    form2.Show();
                
   
                }

            }
        }

        //Metoda aktualizująca wykresy HSV
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
                    ChartType = SeriesChartType.Column
                };
                seriesH.XValueType = ChartValueType.Double;
                chartControl.Series.Add(seriesH);

                foreach (var a in histogramData.HSV_HHisto)
                {
                    seriesH.Points.AddXY(a.Key, a.Value);
                }
            }

            if (chartControl.Name == "HSV_SV")
            {
                var seriesS = new Series
                {
                    Name = "seriesSaturation",
                    Color = Color.Black,
                    IsVisibleInLegend = true,
                    IsXValueIndexed = false,
                    ChartType = SeriesChartType.Column

                };

                seriesS.XValueType = ChartValueType.Double;

                var seriesV = new Series
                {
                    Name = "seriesValue",
                    Color = Color.Red,
                    IsVisibleInLegend = true,
                    IsXValueIndexed = false,
                    ChartType = SeriesChartType.Column
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

        }


        void updateCharts(HistogramData histogramData, Chart chartControl)
        {
         


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

    }
}
