using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrawingTest
{
    public partial class Form3 : Form
    {
        List<TextBox> redTextBoxValues;
        List<TextBox> greenTextBoxValues;
        List<TextBox> blueTextBoxValues;

        int Width;
        int Height;

        Point pointA;
        Point pointB;

        Color[] colorTable;

        Form2 formCaller;

        public Form3(int Height, int Width, Color[] valuesColor, Point pointA, Point pointB, Form formCaller)
        {
            InitializeComponent();
            //initialize TextBox controls for each channel
            redTextBoxValues = new List<TextBox>();
            greenTextBoxValues = new List<TextBox>();
            blueTextBoxValues = new List<TextBox>();

            this.Width = Width;
            this.Height = Height;

            this.pointA = pointA;
            this.pointB = pointB;

            this.formCaller = formCaller as Form2;

            //Add controls
            for(int x = 0; x < Width; x++)
            {
                for(int y = 0; y < Height; y++)
                {

                    redTextBoxValues.Add(new TextBox());
                    redTextBoxValues.Last().BackColor = Color.PaleVioletRed;
                    RedTable.Controls.Add(redTextBoxValues.Last(), x,y);


                    greenTextBoxValues.Add(new TextBox());
                    greenTextBoxValues.Last().BackColor = Color.LightGreen;
                    GreenTable.Controls.Add(greenTextBoxValues.Last(), x, y);


                    blueTextBoxValues.Add(new TextBox());
                    blueTextBoxValues.Last().BackColor = Color.Blue;
                    BlueTable.Controls.Add(blueTextBoxValues.Last(), x, y);
                }
            }

            //Translate to Column wise order form DirectBitmapClass
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                   
                    redTextBoxValues[x+(y*Width)].Text = Convert.ToString(valuesColor[x + (y * Width)].R);
                    greenTextBoxValues[x + (y * Width)].Text = Convert.ToString(valuesColor[x + (y * Width)].G);
                    blueTextBoxValues[x + (y * Width)].Text = Convert.ToString(valuesColor[x + (y * Width)].B);

                }
            }
       


        }


        public void readDataAndAccept()
        {
            DrawingPiece drawingPiece = new DrawingPiece();

            colorTable = new Color[Width * Height];
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    colorTable[x + (y * Width)] = Color.FromArgb(
                                                                Convert.ToInt32(redTextBoxValues[x + (y * Width)].Text),
                                                                Convert.ToInt32(greenTextBoxValues[x + (y * Width)].Text),
                                                                Convert.ToInt32(blueTextBoxValues[x + (y * Width)].Text)
                                                                );
                }
            }

            drawingPiece.colorsToApply = colorTable;
            drawingPiece.pointA = this.pointA;
            drawingPiece.pointB = this.pointB;

            formCaller._drawingPiece = drawingPiece;


        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void AcceptBtn_Click(object sender, EventArgs e)
        {
            this.readDataAndAccept();
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
