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
    //public class HSVBit
    //{
    //    public float HUE { get; set; } //[0-360]
    //    public float saturation { get; set; } //[0-1]
    //    public float value { get; set; } //[0-1]

    //    public static float maxValue(params float[] values)
    //    {
    //        return values.Max();
    //    }

    //    public static float minValue(params float[] values)
    //    {
    //        return values.Min();
    //    }

    //    public Color getColor()
    //    {
    //        float c = this.value * this.saturation;
    //        float x = c * (1 - Math.Abs((this.HUE / 60) % 2 - 1));
    //        float m = this.value - c;

    //        float rPrim = 0;
    //        float gPrim = 0;
    //        float bPrim = 0;

    //        //R' G' B'
    //        if (0 <= this.HUE && 60 >= this.HUE)
    //        {
    //            //C X 0
    //            rPrim = c;
    //            gPrim = x;
    //            bPrim = 0;
    //        }
    //        else if (60 <= this.HUE && 120 >= this.HUE)
    //        {
    //            //X C 0
    //            rPrim = x;
    //            gPrim = c;
    //            bPrim = 0;
    //        }
    //        else if (120 <= this.HUE && 180 >= this.HUE)
    //        {
    //            //0 C X
    //            rPrim = 0;
    //            gPrim = c;
    //            bPrim = x;
    //        }
    //        else if (180 <= this.HUE && 240 >= this.HUE)
    //        {
    //            //0 X C
    //            rPrim = 0;
    //            gPrim = x;
    //            bPrim = c;
    //        }
    //        else if (240 <= this.HUE && 300 >= this.HUE)
    //        {
    //            //X 0 C
    //            rPrim = x;
    //            gPrim = 0;
    //            bPrim = c;
    //        }
    //        else if (300 <= this.HUE && 360 >= this.HUE)
    //        {
    //            //C 0 X
    //            rPrim = c;
    //            gPrim = 0;
    //            bPrim = x;
    //        }

    //        //(R, G, B) = ((R'+m)×255, (G' + m)×255, (B'+m)×255)
    //        return Color.FromArgb((int)((rPrim + m) * 255), (int)((gPrim + m) * 255), (int)((bPrim + m) * 255));


    //    }

    //    static public HSVBit getFromColor(Color color)
    //    {
    //        HSVBit hsvBit = new HSVBit();

    //        float redPrim = (float)color.R / 255;
    //        float greenPrim = (float)color.G / 255;
    //        float bluePrim = (float)color.B / 255;

    //        float cMax = HSVBit.maxValue(redPrim, greenPrim, bluePrim);
    //        float cMin = HSVBit.minValue(redPrim, greenPrim, bluePrim);

    //        float delta = cMax - cMin;

    //        if (delta == 0)
    //        {
    //            hsvBit.HUE = 0;
    //        }
    //        else
    //        {
    //            if (cMax == redPrim)
    //            {
    //                hsvBit.HUE = 60 * (((greenPrim - redPrim) / delta) % 6);
    //            }
    //            else if (cMax == greenPrim)
    //            {
    //                hsvBit.HUE = 60 * ((bluePrim - redPrim) / delta + 2);
    //            }
    //            else if (cMax == bluePrim)
    //            {
    //                hsvBit.HUE = 60 * ((redPrim - greenPrim) / delta + 4);
    //            }
    //        }

    //        if (cMax == 0)
    //        {
    //            hsvBit.saturation = 0;
    //        }
    //        else
    //        {
    //            hsvBit.saturation = delta / cMax;
    //        }

    //        hsvBit.value = cMax;

    //        return hsvBit;
    //    }
    //}


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



        //public void neighborhoodOperation(int[] mask, KernelMethod _method = KernelMethod.NoBorders, SccalingMethod sccalingMethod = SccalingMethod.Cut, Boolean directionEdge = false)
        //{
        //    int maskSize = (int)Math.Sqrt(mask.Length);
        //    int calculatedPixel = 0;
        //    int maskSum = mask.Min() < 0 ? 1 : mask.Sum();
        //    int maskSumInner = 0;

        //    int[] bitMapToScale = null;
        //    if (sccalingMethod == SccalingMethod.Scale)
        //    {
        //        bitMapToScale = new int[directBitmapPre.Height * directBitmapPre.Width];
        //    }



        //    int minPixel = directBitmapPre.getMin();
        //    int maxPixel = directBitmapPre.getMax();


        //    int maskValue;

        //    int xo;
        //    int yo;

        //    int lowerXOverlap = 0;
        //    int higherXOverlap = 0;
        //    int lowerYOverlap = 0;
        //    int higherYOverlap = 0;

        //    int yiInner = 0;

        //    int calculatedPixelIndexX = 0;
        //    int calculatedPixelIndexY = 0;

        //    Color helpColor;

        //    int overlap = 0; // = maskSize / 2;
        //    int maskOverlap = maskSize / 2;
        //    switch (_method)
        //    {
        //        case KernelMethod.NoBorders:
        //            overlap = maskSize / 2;
        //            break;
        //        case KernelMethod.CloneBorder:
        //            overlap = 0;
        //            break;
        //        case KernelMethod.UseExisting:
        //            overlap = 0;
        //            break;

        //    }



        //    //Iterate through image
        //    for (int x = overlap; x < directBitmapPre.Width - overlap; ++x)
        //    {
        //        for (int y = overlap; y < directBitmapPre.Height - overlap; ++y)
        //        {
        //            //Iterate through image

        //            //Iterate through mask
        //            for (int xi = 0; xi < maskSize; xi++)
        //            {
        //                for (int yi = 0; yi < maskSize; yi++)
        //                {

        //                    maskValue = mask[xi + (yi * maskSize)];
        //                    xo = x + xi - maskOverlap;
        //                    yo = y + yi - maskOverlap;

        //                    calculatedPixelIndexX = x + xi - maskOverlap;
        //                    calculatedPixelIndexY = y + yi - maskOverlap;

        //                    if ((x == 0 && y == 0) || (x == directBitmapPre.Width && y == directBitmapPre.Height))
        //                    {
        //                        higherXOverlap = 0;
        //                    }


        //                    if (_method == KernelMethod.UseExisting && (calculatedPixelIndexX < 0 || calculatedPixelIndexY < 0 || calculatedPixelIndexX >= directBitmapPre.Width || calculatedPixelIndexY >= directBitmapPre.Height))
        //                    {
        //                        continue;
        //                    }

        //                    if (_method == KernelMethod.CloneBorder && (calculatedPixelIndexX < 0 || calculatedPixelIndexY < 0 || calculatedPixelIndexX >= directBitmapPre.Width || calculatedPixelIndexY >= directBitmapPre.Height))
        //                    {
        //                        calculatedPixel += (directBitmapPre.GetPixel(x, y).R * mask[xi + (yi * maskSize)]);

        //                        continue;
        //                    }
        //                    maskSumInner += mask[xi + (yi * maskSize)];
        //                    calculatedPixel += (directBitmapPre.GetPixel(calculatedPixelIndexX, calculatedPixelIndexY).R * mask[xi + (yi * maskSize)]);


        //                }
        //            }

        //            if (_method == KernelMethod.UseExisting)
        //            {
        //                calculatedPixel = maskSum == 1 ? calculatedPixel : calculatedPixel / maskSumInner;
        //            }
        //            else
        //            {
        //                calculatedPixel /= maskSum;
        //            }

        //            int newPixel = calculatedPixel;

        //            if (sccalingMethod != SccalingMethod.Scale)
        //            {
        //                newPixel = this.ScalePixelValue(newPixel, sccalingMethod, sccalingMethod == SccalingMethod.Scale ? minPixel : 0, sccalingMethod == SccalingMethod.Scale ? maxPixel : 255);

        //            }


        //            if (sccalingMethod != SccalingMethod.Scale)
        //            {
        //                directBitmapPost.SetPixel(x, y, Color.FromArgb(newPixel, newPixel, newPixel));
        //            }
        //            else
        //            {
        //                bitMapToScale[x + (y * directBitmapPre.Width)] = newPixel;
        //            }
        //            maskSumInner = 0;
        //            calculatedPixel = 0;
        //        }
        //    }

        //    if (sccalingMethod == SccalingMethod.Scale)
        //    {
        //        int minValue = bitMapToScale.Min();
        //        int maxValue = bitMapToScale.Max();
        //        int pixelScalled;
        //        List<int> checkList = new List<int>();



        //        for (int x = 0; x < directBitmapPre.Width - overlap; ++x)
        //        {
        //            for (int y = 0; y < directBitmapPre.Height - overlap; ++y)
        //            {
        //                pixelScalled = (int)(((decimal)(bitMapToScale[x + (y * directBitmapPre.Width)] - minValue) / (decimal)(maxValue - minValue)) * 255);
        //                checkList.Add(pixelScalled);
        //                directBitmapPost.SetPixel(x, y, Color.FromArgb(pixelScalled, pixelScalled, pixelScalled));
        //            }
        //        }
        //    }

        //}

        public void neighborhoodOperationHSV(int[] mask, DirectBitmap directBitmapPre, DirectBitmap directBitmapPost, KernelMethod _method = KernelMethod.NoBorders, SccalingMethod sccalingMethod = SccalingMethod.Cut, Boolean directionEdge = false)
        {
            int maskSize = (int)Math.Sqrt(mask.Length);
            int calculatedPixel = 0;
            HSVBit calculatedHSVBit = new HSVBit();
            int maskSum = mask.Min() < 0 ? 1 : mask.Sum();
            int maskSumInner = 0;

            List<float> angles = new List<float>();

            int[] bitMapToScale = null;
            if (sccalingMethod == SccalingMethod.Scale)
            {
                bitMapToScale = new int[directBitmapPre.Height * directBitmapPre.Width];
            }



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
                             
                                //calculatedPixel += (directBitmapPre.GetPixel(x, y).R * mask[xi + (yi * maskSize)]);

                                for(int i = 0; i < cloneIndexValue; i++)
                                {
                                    angles.Add(directBitmapPre.GetPixelHSV(x, y).HUE);
                                }
                                
                                //calculatedHSVBit.HUE += (directBitmapPre.GetPixelHSV(x, y).HUE * cloneIndexValue);
                                calculatedHSVBit.saturation += (directBitmapPre.GetPixelHSV(xi, yi).saturation * cloneIndexValue);
                                calculatedHSVBit.value += (directBitmapPre.GetPixelHSV(xi, yi).saturation * cloneIndexValue);
                                continue;
                            }

                            var cloneIndexValueMain = mask[xi + (yi * maskSize)];
                            maskSumInner += cloneIndexValueMain;
                            //calculatedPixel += (directBitmapPre.GetPixel(calculatedPixelIndexX, calculatedPixelIndexY).R * mask[xi + (yi * maskSize)]);

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
                        //calculatedHSVBit.value = bit.value;
                        calculatedHSVBit.HUE = bit.HUE;
                        //directBitmapPost.SetPixel(x,y)
                         if (calculatedHSVBit.HUE != bit.HUE)
                             bit = null;

                        //if (calculatedHSVBit.saturation != bit.saturation)
                        //    bit = null;
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

            if (sccalingMethod == SccalingMethod.Scale)
            {
                int minValue = bitMapToScale.Min();
                int maxValue = bitMapToScale.Max();
                int pixelScalled;
                List<int> checkList = new List<int>();



                for (int x = 0; x < directBitmapPre.Width - overlap; ++x)
                {
                    for (int y = 0; y < directBitmapPre.Height - overlap; ++y)
                    {
                        pixelScalled = (int)(((decimal)(bitMapToScale[x + (y * directBitmapPre.Width)] - minValue) / (decimal)(maxValue - minValue)) * 255);
                        checkList.Add(pixelScalled);
                        directBitmapPost.SetPixel(x, y, Color.FromArgb(pixelScalled, pixelScalled, pixelScalled));
                    }
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
        public void TURN_BLACK(DirectBitmap directBitmapPost)
        {
            for (int x = 0; x < directBitmapPost.Width ; ++x)
            {
                for (int y = 0; y < directBitmapPost.Height ; ++y)
                {
                    directBitmapPost.SetPixel(x, y, Color.Black);
                }
            }
        }

    }


    
}
