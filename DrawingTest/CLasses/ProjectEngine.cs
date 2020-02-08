using ImageProcessingMM.EngineClasses;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Math;
namespace DrawingTest.CLasses
{
    public enum KernelMethod
    {
        NoBorders,
        CloneBorder,
        UseExisting
    };

    public enum SccalingMethod
    {
        Cut,
        Scale,
        TriValue
    };

    public class DrawnPoint
    {
        public Color color { get; set; }
        public Point point { get; set; }

        public DrawnPoint(Color color, Point point)
        {
            this.color = color;
            this.point = point;
        }
    }
 


public class ProjectEngine
    {

        //public DirectBitmap directBitmapPre { get; set; }
        //public DirectBitmap directBitmapPost { get; set; }

        float MeanAngle(float[] angles)
        {
            var x = angles.Sum(a => Cos(a * PI / 180)) / angles.Length;
            var y = angles.Sum(a => Sin(a * PI / 180)) / angles.Length;

            var result = (float)(Atan2(y, x) * 180 / PI);
           
            if(result < 0)
            {
                result += 360;
            }

            return result;
        }




        static public HSVBit getFromColor(Color color)
        {
            HSVBit hsvBit = new HSVBit();

            float redPrim = (float)color.R / 255;
            float greenPrim = (float)color.G / 255;
            float bluePrim = (float)color.B / 255;

            float cMax = HSVBit.maxValue(redPrim, greenPrim, bluePrim);
            float cMin = HSVBit.minValue(redPrim, greenPrim, bluePrim);

            float delta = cMax - cMin;

            if (delta == 0)
            {
                hsvBit.HUE = 0;
            }
            else
            {
                if (cMax == redPrim)
                {
                    hsvBit.HUE = 60 * (((greenPrim - redPrim) / delta) % 6);
                }
                else if (cMax == greenPrim)
                {
                    hsvBit.HUE = 60 * ((bluePrim - redPrim) / delta + 2);
                }
                else if (cMax == bluePrim)
                {
                    hsvBit.HUE = 60 * ((redPrim - greenPrim) / delta + 4);
                }
            }

            if (cMax == 0)
            {
                hsvBit.saturation = 0;
            }
            else
            {
                hsvBit.saturation = delta / cMax;
            }

            hsvBit.value = cMax;

            return hsvBit;
        }



       
        public void neighborhoodOperationHSV(int[] mask, DirectBitmap directBitmapPre, DirectBitmap directBitmapPost, KernelMethod _method = KernelMethod.NoBorders, SccalingMethod sccalingMethod = SccalingMethod.Cut, Boolean directionEdge = false)
        {
            int maskSize = (int)Math.Sqrt(mask.Length);
            int calculatedPixel = 0;
            HSVBit calculatedHSVBit = new HSVBit();
            int maskSum = mask.Min() < 0 ? 1 : mask.Sum();
            int maskSumInner = 0;

            List<float> angles = new List<float>();

            int[] bitMapToScale = null;
      
            int minPixel = directBitmapPre.getMin();
            int maxPixel = directBitmapPre.getMax();

            int maskValue;

            int xo;
            int yo;

            int lowerXOverlap = 0;
            int higherXOverlap = 0;
            int lowerYOverlap = 0;
            int higherYOverlap = 0;

            int yiInner = 0;

            int calculatedPixelIndexX = 0;
            int calculatedPixelIndexY = 0;

            Color helpColor;
            HSVBit helperhSVBit;

            int overlap = 0; // = maskSize / 2;
            int maskOverlap = maskSize / 2;
            switch (_method)
            {
                case KernelMethod.NoBorders:
                    overlap = maskSize / 2;
                    break;
                case KernelMethod.CloneBorder:
                    overlap = 0;
                    break;
                case KernelMethod.UseExisting:
                    overlap = 0;
                    break;

            }



            //Iterate through image
            for (int x = overlap; x < directBitmapPre.Width - overlap; ++x)
            {
                for (int y = overlap; y < directBitmapPre.Height - overlap; ++y)
                {
                    //Iterate through image

                    //Iterate through mask
                    for (int xi = 0; xi < maskSize; xi++)
                    {
                        for (int yi = 0; yi < maskSize; yi++)
                        {

                            maskValue = mask[xi + (yi * maskSize)];
                            xo = x + xi - maskOverlap;
                            yo = y + yi - maskOverlap;

                            calculatedPixelIndexX = x + xi - maskOverlap;
                            calculatedPixelIndexY = y + yi - maskOverlap;

                            if ((x == 0 && y == 0) || (x == directBitmapPre.Width && y == directBitmapPre.Height))
                            {
                                higherXOverlap = 0;
                            }


                            if (_method == KernelMethod.UseExisting && (calculatedPixelIndexX < 0 || calculatedPixelIndexY < 0 || calculatedPixelIndexX >= directBitmapPre.Width || calculatedPixelIndexY >= directBitmapPre.Height))
                            {
                                continue;
                            }

                            if (_method == KernelMethod.CloneBorder && (calculatedPixelIndexX < 0 || calculatedPixelIndexY < 0 || calculatedPixelIndexX >= directBitmapPre.Width || calculatedPixelIndexY >= directBitmapPre.Height))
                            {
                                var cloneIndexValue = mask[xi + (yi * maskSize)];
                             
                              

                                for(int i = 0; i < cloneIndexValue; i++)
                                {
                                    angles.Add(directBitmapPre.GetPixelHSV(x, y).HUE);
                                }
                                
                                calculatedHSVBit.value += (directBitmapPre.GetPixelHSV(xi, yi).saturation * cloneIndexValue);
                                continue;
                            }

                            var cloneIndexValueMain = mask[xi + (yi * maskSize)];
                            maskSumInner += cloneIndexValueMain;
            

                            for (int i = 0; i < cloneIndexValueMain; i++)
                            {
                                angles.Add(directBitmapPre.GetPixelHSV(calculatedPixelIndexX, calculatedPixelIndexY).HUE);
                            }

                            //calculatedHSVBit.HUE += (directBitmapPre.GetPixelHSV(x, y).HUE * cloneIndexValueMain);
                            calculatedHSVBit.saturation += (directBitmapPre.GetPixelHSV(calculatedPixelIndexX, calculatedPixelIndexY).saturation * cloneIndexValueMain);
                            calculatedHSVBit.value += (directBitmapPre.GetPixelHSV(calculatedPixelIndexX, calculatedPixelIndexY).value * cloneIndexValueMain);

                        }
                    }

                    if (_method == KernelMethod.UseExisting)
                    {
                        calculatedHSVBit.HUE = this.MeanAngle(angles.ToArray());
                        calculatedHSVBit.saturation = maskSum == 1 ? calculatedHSVBit.saturation : calculatedHSVBit.saturation / maskSumInner;
                        calculatedHSVBit.value = maskSum == 1 ? calculatedHSVBit.value : calculatedHSVBit.value / maskSumInner;
                        //calculatedPixel = maskSum == 1 ? calculatedPixel : calculatedPixel / maskSumInner;
                    }
                    else
                    {
                        var bit = directBitmapPost.GetPixelHSV(x, y);
                        // calculatedPixel /= maskSum;
                        calculatedHSVBit.HUE = this.MeanAngle(angles.ToArray());
                        calculatedHSVBit.saturation = maskSum == 1 ? calculatedHSVBit.saturation : calculatedHSVBit.saturation / maskSumInner;
                        calculatedHSVBit.value = maskSum == 1 ? calculatedHSVBit.value : calculatedHSVBit.value / maskSumInner;
                    }

                    int newPixel = calculatedPixel;

                    if (sccalingMethod != SccalingMethod.Scale)
                    {
                        newPixel = this.ScalePixelValue(newPixel, sccalingMethod, sccalingMethod == SccalingMethod.Scale ? minPixel : 0, sccalingMethod == SccalingMethod.Scale ? maxPixel : 255);

                    }


                    if (sccalingMethod != SccalingMethod.Scale)
                    {
                        var bit = directBitmapPost.GetPixelHSV(x, y);
                      
                        calculatedHSVBit.saturation = bit.saturation;
                   
                        calculatedHSVBit.HUE = bit.HUE;
                        //apply cut scalling
                        if (calculatedHSVBit.value > 1)
                            calculatedHSVBit.value = 1;

                        if (calculatedHSVBit.value < 0)
                            calculatedHSVBit.value = 0;
                     
                        directBitmapPost.SetPixel(x, y, calculatedHSVBit.getColor());
                    }
                    else
                    {
                        bitMapToScale[x + (y * directBitmapPre.Width)] = newPixel;
                    }
                    maskSumInner = 0;
                    calculatedPixel = 0;
                    calculatedHSVBit.HUE = 0;
                    calculatedHSVBit.saturation = 0;
                    calculatedHSVBit.value = 0;
                    angles.Clear();
                }
            }

           

        }
        private int ScalePixelValue(int pixel, SccalingMethod method, int min = 0, int max = 255)
        {
            int returnValue = 0;

            switch (method)
            {
                case SccalingMethod.Cut:
                    if (pixel > 255)
                    {
                        returnValue = 255;
                    }
                    else if (pixel < 0)
                    {
                        returnValue = 0;
                    }
                    else
                    {
                        returnValue = pixel;
                    }

                    break;

                case SccalingMethod.Scale:
                    returnValue = (int)(((decimal)(pixel - min) / (decimal)(max - min)) * 255);
                    break;

                case SccalingMethod.TriValue:
                    if (pixel > 255)
                    {
                        returnValue = 255;
                    }
                    else if (pixel < 0)
                    {
                        returnValue = 0;
                    }
                    else
                    {
                        returnValue = 128; // 255/2
                    }
                    break;
            }

            return returnValue;
        }
      

    }


    
}
