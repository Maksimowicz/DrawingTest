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

namespace DrawingTest
{
    public class DrawingPiece
    {
        public Color[] colorsToApply { get; set; }
        public Point pointA { get; set; }
        public Point pointB { get; set; }
    }




    public enum DrawingType
    {
        FreeHand,
        Line,
        ManyLines,
        Retclange

    }



    public partial class Form2 : Form
    {

        ProjectEngine projectEngine;
        DrawingType drawingType { get; set; }
        DirectBitmap directBitmap { get; set; }
        DirectBitmap directBitmapOrig { get; set; }
        DirectBitmap directBitmapPost { get; set; }
        
        public DrawingPiece drawingPiece { get; set; }
        public DrawingPiece _drawingPiece
        {
            get
            {
                return drawingPiece;
            }
            set
            {
                drawingPiece = value;
                //TODO: funcka rysująca zmiany
            }
        }
       
    


    public Image ImageToShow { get; set; }
        public Form1 formCaller { get; set; }
        Bitmap pictureOriginal { get; set; }
      

        HistogramData histogramDataRGBMain { get; set; }
        HistogramData histogramDataLineRGB { get; set; }
        HistogramData histogramDataHSVMain { get; set; }

        Boolean isFreeHand;
        class DrawnPoint
        {
            public Color color { get; set; }
            public Point point { get; set; }
          

            public DrawnPoint(Color color, Point point)
            {
                this.color = color;
                this.point = point;
            }
        }
        Point _nextPoint;
        Point _lastPoint;

        Point lastPoint
        {
            get { return _lastPoint; }
            set
            {
                _lastPoint = value;
                if (_lastPoint != null)
                {
                    numericUpDown1.Value = _lastPoint.X;
                    numericUpDown2.Value = _lastPoint.Y;
                }

            }
        }

        private void readFromFrom3()
        {
            int XStart = 0;
            int XEnd = 0;

            int YStart = 0;
            int YEnd = 0;

            var differenceX = drawingPiece.pointA.X - drawingPiece.pointB.X;
            var differenceY = drawingPiece.pointA.Y - drawingPiece.pointB.Y;

            Point pointAInner = drawingPiece.pointA;
            Point pointBInner = drawingPiece.pointB;

            if (Math.Abs(differenceX) > 20)
            {
                if (differenceX > 0)
                {
                   
                    XStart = pointAInner.X;
                    XEnd = pointBInner.X;

                }
                else
                {
               
                    XStart = pointBInner.X;
                    XEnd = pointAInner.X;
                }
            }

            if (Math.Abs(differenceY) > 20)
            {
                if (differenceY > 0)
                {
                    YStart = pointAInner.Y;
                    YEnd = pointBInner.Y;
                }
                else
                {
                    YStart = pointBInner.Y;
                    YEnd = pointAInner.Y;
                }
            }
            int xInner = 0;
            int yInner = 0;

            for (int x = XStart; x < XEnd; x++)
            {
                for (int y = YStart; y < YEnd; y++)
                {
                    directBitmap.SetPixel(x, y, drawingPiece.colorsToApply[x + (y * Width)]);
                }
                yInner = 0;
                xInner++;
            }
        }



        //  = Point.Empty;//Point.Empty represents null for a Point object

        bool isMouseDown = new Boolean();//this is used to evaluate whether our mousebutton is down or not

        List<DrawnPoint> drawnPointList;
        public Form2(Form formCaller)
        {
            this.formCaller = formCaller as Form1; 
            InitializeComponent();
            this.MaximumSize = SystemInformation.PrimaryMonitorMaximizedWindowSize;
            drawnPointList = new List<DrawnPoint>();
            
          
          
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            pictureBox1.Image = ImageToShow;
            pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
            pictureOriginal = (Bitmap)ImageToShow.Clone();
            directBitmap = new DirectBitmap(pictureOriginal);
            directBitmapOrig = new DirectBitmap(directBitmap);
            directBitmapPost = new DirectBitmap(directBitmap);
            histogramDataLineRGB = new HistogramData();
            directBitmapOrig.generateHSVBits();
            formCaller.histogramDataRGBMainProp = directBitmap.generateHistogram();
            formCaller.histogramDataHSVProp = directBitmap.generateHistogramHSV();
            projectEngine = new ProjectEngine();
            //projectEngine.directBitmapPre = new DirectBitmap(directBitmapOrig);
            //projectEngine.directBitmapPost = new DirectBitmap(directBitmapOrig);
            //projectEngine.directBitmapPre.generateHSVBits();
            //projectEngine.directBitmapPost.generateHSVBits();

            //this.
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)

        {
            List<Point> listOfDrawnPoints;
            int helperValue = 0;
            switch (drawingType)
            {


                case DrawingType.Retclange:
                    int XStart = 0;
                    int XEnd = 0;

                    int YStart = 0;
                    int YEnd = 0;


                    if(lastPoint == Point.Empty)
                    {
                        lastPoint = e.Location;
                    }
                    else
                    {
                        _nextPoint = e.Location;
                        var differenceX = _nextPoint.X - lastPoint.X;
                        var differenceY = _nextPoint.Y - lastPoint.Y;
                        if(Math.Abs(differenceX) > 20)
                        {
                            if(differenceX > 0)
                            {
                                _nextPoint.X = lastPoint.X + 20;
                                XStart = lastPoint.X;
                                XEnd = _nextPoint.X;

                            }
                            else
                            {
                                _nextPoint.X = lastPoint.X - 20;
                                XStart = _nextPoint.X;
                                XEnd = lastPoint.X;
                            }
                        }

                        if(Math.Abs(differenceY) > 20)
                        {
                            if(differenceY > 0)
                            {
                                _nextPoint.Y = lastPoint.Y + 20;
                                YStart = lastPoint.Y;
                                YEnd = _nextPoint.Y;
                            }
                            else
                            {
                                _nextPoint.Y = lastPoint.Y - 20;
                                YStart = _nextPoint.Y;
                                YEnd = lastPoint.Y;
                            }
                        }
                    }


                    if(_nextPoint != Point.Empty)
                    {
                        listOfDrawnPoints = this.EnumerateLineNoDiagonalSteps(lastPoint.X, lastPoint.Y, lastPoint.X, _nextPoint.Y);
                        listOfDrawnPoints.AddRange(this.EnumerateLineNoDiagonalSteps(lastPoint.X, _nextPoint.Y, _nextPoint.X, _nextPoint.Y));
                        listOfDrawnPoints.AddRange(this.EnumerateLineNoDiagonalSteps(_nextPoint.X, _nextPoint.Y, _nextPoint.X, lastPoint.Y));
                        listOfDrawnPoints.AddRange(this.EnumerateLineNoDiagonalSteps(_nextPoint.X, lastPoint.Y, lastPoint.X, lastPoint.Y));

                        foreach(var x in listOfDrawnPoints)
                        {
                            drawnPointList.Add(new DrawnPoint(directBitmapOrig.GetPixel(x.X, x.Y), x));
                        }
                        foreach (var x in listOfDrawnPoints)
                        {
                            // drawnPointList.Add(new DrawnPoint(bitmap.GetPixel(x.X,x.Y), x));

                            directBitmap.SetPixel(x.X, x.Y, Color.Black);
                        }
                        using (Graphics g = Graphics.FromImage(pictureBox1.Image))
                        {
                            g.DrawImage(directBitmap.Bitmap, new Point(0, 0));
                        }
                        pictureBox1.Invalidate();

                        _nextPoint = Point.Empty;
                        _lastPoint = Point.Empty;


                        int Width = Math.Abs(XStart - XEnd);
                        int Height = Math.Abs(YStart - YEnd);
                      

                        Color[] valuesToPass = new Color[Width * Height];

                        int xInner = 0;
                        int yInner = 0;

                        for (int x = XStart; x < XEnd; x++)
                        {
                            for (int y = YStart; y < YEnd; y++)
                            {
                                valuesToPass[xInner + (yInner * Width)] = directBitmapOrig.GetPixel(x, y);
                               
                                yInner++;
                            }
                            yInner = 0;
                            xInner++;
                        }
                        
                        Form3 form3 = new Form3(Height, Width, valuesToPass, lastPoint, _nextPoint, this);
                        form3.Show();

                       

                    }


        
                    break;

                   

                case DrawingType.FreeHand:
                    lastPoint = e.Location;//we assign the lastPoint to the current mouse position: e.Location ('e' is from the MouseEventArgs passed into the MouseDown event)
                   
                    
                    
                    drawnPointList.Add(new DrawnPoint(Color.Pink, lastPoint));
                    isMouseDown = true;//we set to true because our mouse button is down (clicked)
                    break;

                case DrawingType.Line:
                    if(lastPoint == Point.Empty)
                    {
                        lastPoint = e.Location;
                    }
                    else
                    {
                        _nextPoint = e.Location;
                        using (Graphics g = Graphics.FromImage(pictureBox1.Image))
                        {

                            listOfDrawnPoints = this.EnumerateLineNoDiagonalSteps(lastPoint.X, lastPoint.Y, _nextPoint.X, _nextPoint.Y);

                            foreach (var x in listOfDrawnPoints)
                            {
                                Color color = directBitmapOrig.GetPixel(x.X, x.Y);
                                HSVBit hsvBit = directBitmapOrig.GetPixelHSV(x.X, x.Y);
                                drawnPointList.Add(new DrawnPoint(directBitmapOrig.GetPixel(x.X, x.Y), x));
                                histogramDataLineRGB.redHisto[color.R]++;
                                histogramDataLineRGB.blueHisto[color.B]++;
                                histogramDataLineRGB.greenHisto[color.G]++;


                                //histogramData.HSV_HHisto.TryGetValue(hsvBitHelp.HUE, out helperValue);  
                                //histogramData.HSV_HHisto[hsvBitHelp.HUE] = helperValue + 1;

                                //histogramData.HSV_SHisto.TryGetValue(hsvBitHelp.saturation, out helperValue);
                                //histogramData.HSV_SHisto[hsvBitHelp.saturation] = helperValue + 1;

                                //histogramData.HSV_VHisto.TryGetValue(hsvBitHelp.value, out helperValue);
                                //histogramData.HSV_VHisto[hsvBitHelp.value] = helperValue + 1;

                                histogramDataLineRGB.HSV_HHisto.TryGetValue(hsvBit.HUE, out helperValue);
                                histogramDataLineRGB.HSV_HHisto[hsvBit.HUE] = helperValue + 1;

                                histogramDataLineRGB.HSV_SHisto.TryGetValue(hsvBit.saturation, out helperValue);
                                histogramDataLineRGB.HSV_SHisto[hsvBit.saturation] = helperValue + 1;

                                histogramDataLineRGB.HSV_VHisto.TryGetValue(hsvBit.value, out helperValue);
                                histogramDataLineRGB.HSV_VHisto[hsvBit.value] = helperValue + 1;


                                //formCaller.histogramDataLineRGBProp = histogramDataLineRGB;

                                formCaller.ser1.Points.AddXY(color.R, histogramDataLineRGB.redHisto[color.R]);
                                formCaller.ser2.Points.AddXY(color.G, histogramDataLineRGB.greenHisto[color.G]);
                                formCaller.ser3.Points.AddXY(color.B, histogramDataLineRGB.blueHisto[color.B]);

                                formCaller.seriesHExt.Points.AddXY(hsvBit.HUE, histogramDataLineRGB.HSV_HHisto[hsvBit.HUE]);
                                formCaller.seriesSExt.Points.AddXY(hsvBit.saturation, histogramDataLineRGB.HSV_SHisto[hsvBit.saturation]);
                                formCaller.seriesVExt.Points.AddXY(hsvBit.value, histogramDataLineRGB.HSV_VHisto[hsvBit.value]);

                               // formCaller.seriesHExt.AddXY()
                                



                            }

                            foreach (var x in listOfDrawnPoints)
                            {
                                // drawnPointList.Add(new DrawnPoint(bitmap.GetPixel(x.X,x.Y), x));

                                directBitmap.SetPixel(x.X, x.Y, Color.Black);
                            }
                            g.DrawImage(directBitmap.Bitmap, new Point(0, 0));

                        }

                        pictureBox1.Invalidate();//refreshes the picturebox
                        lastPoint = new Point();
                        //lastPoint = e.Location;//keep assigning the lastPoint to the current mouse position
                        drawnPointList.Add(new DrawnPoint(directBitmap.GetPixel(lastPoint.X, lastPoint.Y), lastPoint));
                    }
                    break;

                case DrawingType.ManyLines:
                    if (lastPoint == Point.Empty)
                    {
                        lastPoint = e.Location;
                    }
                    else
                    {
                        _nextPoint = e.Location;


                        numericUpDown3.Value = _nextPoint.X;
                        numericUpDown4.Value = _nextPoint.Y;
                        using (Graphics g = Graphics.FromImage(pictureBox1.Image))
                        {

                            listOfDrawnPoints = this.EnumerateLineNoDiagonalSteps(lastPoint.X, lastPoint.Y, _nextPoint.X, _nextPoint.Y);

                            foreach (var x in listOfDrawnPoints)
                            {
                                Color color = directBitmapOrig.GetPixel(x.X, x.Y);
                                drawnPointList.Add(new DrawnPoint(directBitmapOrig.GetPixel(x.X, x.Y), x));
                                histogramDataLineRGB.redHisto[color.R]++;
                                histogramDataLineRGB.blueHisto[color.B]++;
                                histogramDataLineRGB.greenHisto[color.G]++;
                                //formCaller.histogramDataLineRGBProp = histogramDataLineRGB;

                                formCaller.ser1.Points.AddXY(color.R, histogramDataLineRGB.redHisto[color.R]);
                                formCaller.ser2.Points.AddXY(color.G, histogramDataLineRGB.greenHisto[color.G]);
                                formCaller.ser3.Points.AddXY(color.B, histogramDataLineRGB.blueHisto[color.B]);
                            }

                            foreach (var x in listOfDrawnPoints)
                            {
                                // drawnPointList.Add(new DrawnPoint(bitmap.GetPixel(x.X,x.Y), x));

                                directBitmap.SetPixel(x.X, x.Y, Color.Black);
                            }
                            g.DrawImage(directBitmap.Bitmap, new Point(0, 0));

                        }

                        pictureBox1.Invalidate();//refreshes the picturebox
                                                 // lastPoint = new Point();
                        lastPoint = _nextPoint;//keep assigning the lastPoint to the current mouse position
                        drawnPointList.Add(new DrawnPoint(directBitmap.GetPixel(lastPoint.X, lastPoint.Y), lastPoint));
                    }
                    break;
            }
        


        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)

        {
            List<Point> listOfDrawnPoints;
            //Bitmap bitmap = (Bitmap)pictureBox1.Image;
            //Bitmap bitmap2 = (Bitmap)bitmap.Clone();
            if (isMouseDown == true && drawingType == DrawingType.FreeHand)//check to see if the mouse button is down

            {

                if (lastPoint != null)//if our last point is not null, which in this case we have assigned above

                {

                    if (pictureBox1.Image == null)//if no available bitmap exists on the picturebox to draw on

                    {
                        //create a new bitmap
                       // Bitmap bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);

                       // pictureBox1.Image = bmp; //assign the picturebox.Image property to the bitmap created

                    }

                    using (Graphics g = Graphics.FromImage(pictureBox1.Image))
                    {

                        listOfDrawnPoints = this.EnumerateLineNoDiagonalSteps(lastPoint.X, lastPoint.Y, e.X, e.Y);

                        foreach (var x in listOfDrawnPoints)
                        {
                            Color color = directBitmapOrig.GetPixel(x.X, x.Y);
                            drawnPointList.Add(new DrawnPoint(directBitmapOrig.GetPixel(x.X, x.Y), x));
                            histogramDataLineRGB.redHisto[color.R]++;
                            histogramDataLineRGB.blueHisto[color.B]++;
                            histogramDataLineRGB.greenHisto[color.G]++;
                            //formCaller.histogramDataLineRGBProp = histogramDataLineRGB;

                            formCaller.ser1.Points.AddXY(color.R, histogramDataLineRGB.redHisto[color.R]);
                            formCaller.ser2.Points.AddXY(color.G, histogramDataLineRGB.greenHisto[color.G]);
                            formCaller.ser3.Points.AddXY(color.B, histogramDataLineRGB.blueHisto[color.B]);
                        }

                        foreach (var x in listOfDrawnPoints)
                        {
                           // drawnPointList.Add(new DrawnPoint(bitmap.GetPixel(x.X,x.Y), x));
                           
                            directBitmap.SetPixel(x.X, x.Y, Color.Black);
                        }
                        g.DrawImage(directBitmap.Bitmap, new Point(0, 0));

                    }
                  
                    pictureBox1.Invalidate();//refreshes the picturebox

                    lastPoint = e.Location;//keep assigning the lastPoint to the current mouse position
                    drawnPointList.Add(new DrawnPoint(directBitmap.GetPixel(lastPoint.X,lastPoint.Y), lastPoint));


                   
                }

            }

        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)

        {

            isMouseDown = false;
            if (false)
            {
                lastPoint = Point.Empty;
            }

            //set the previous point back to null if the user lets go of the mouse button

        }

        private List<Point> EnumerateLineNoDiagonalSteps(int x0, int y0, int x1, int y1)
        {
            //List<Tuple<int, int>> pointList = new List<Tuple<int, int>>();
            List<Point> pointList = new List<Point>();
            int dx = Math.Abs(x1 - x0), sx = x0 < x1 ? 1 : -1;
            int dy = -Math.Abs(y1 - y0), sy = y0 < y1 ? 1 : -1;
            int err = dx + dy, e2;

            while (true)
            {
                //yield return Tuple.Create(x0, y0);
                pointList.Add(new Point(x0, y0));
                if (x0 == x1 && y0 == y1) break;

                e2 = 2 * err;

                // EITHER horizontal OR vertical step (but not both!)
                if (e2 > dy)
                {
                    err += dy;
                    x0 += sx;
                }
                else if (e2 < dx)
                { // <--- this "else" makes the difference
                    err += dx;
                    y0 += sy;
                }
            }

            return pointList;
        }

        private void tEKSTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap bitmap = (Bitmap)pictureBox1.Image;
            foreach (var point in drawnPointList)
            {
                directBitmap.SetPixel(point.point.X, point.point.Y, point.color);

            }


            using (Graphics g = Graphics.FromImage(pictureBox1.Image))
            {
                g.DrawImage(directBitmap.Bitmap, new Point(0, 0));
            }


            pictureBox1.Invalidate();
            //pictureBox1.Image = directBitmap.Bitmap;
            drawnPointList.Clear();

            formCaller.ser1.Points.Clear();
            formCaller.ser2.Points.Clear();
            formCaller.ser3.Points.Clear();

            lastPoint = Point.Empty;

        }


        private void button1_Click(object sender, EventArgs e)
        {
           
            //numericUpDown1.Value = 0;
        }

        private void drawFreehandToolStripMenuItem_Click(object sender, EventArgs e)
        {
            drawingType = DrawingType.FreeHand;
        }

        private void drawLineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            drawingType = DrawingType.Line;
        }

        private void drawManyLlinesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            drawingType = DrawingType.ManyLines;
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            //int[] mask = { 0, -1, 0, -1, 5, -1, 0, -1, 0 };
            //projectEngine.neighborhoodOperation(mask, KernelMethod.NoBorders, SccalingMethod.Cut);

            //using (Graphics g = Graphics.FromImage(pictureBox1.Image))
            //{
            //    g.DrawImage(projectEngine.directBitmapPost.Bitmap, new Point(0, 0));
            //}
           
        }

        private void toolStripMenuItem2_Click_1(object sender, EventArgs e)
        {
            //  int[] mask = { 1, 1, 1, 1, 1, 1, 1, 1, 1 };
            int[] mask = { -1, -1, -1, -1, 9, -1, -1, -1, -1 };
            projectEngine.neighborhoodOperationHSV(mask, directBitmapOrig, directBitmap);
            
            using (Graphics g = Graphics.FromImage(pictureBox1.Image))
            {
                g.DrawImage(directBitmap.Bitmap, new Point(0, 0));
            }


            pictureBox1.Invalidate();

            directBitmapOrig = new DirectBitmap(directBitmap);
            directBitmapOrig.generateHSVBits();

            

            
        }

        private void editPixels20x20ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            drawingType = DrawingType.Retclange;
            _nextPoint = Point.Empty;
            _lastPoint = Point.Empty;

            
        }
    }
}
