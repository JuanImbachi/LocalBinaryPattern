using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace LBP_Arquihard.Model
{
    class Images
    {
        private Bitmap bitMap;
        private Bitmap bitMapLbp;
        public Images()
        {

        }

        public Bitmap GetBitmapLBP()
        {
            return bitMapLbp;
        }

        public int[,] lbp(int radius, int numNeighbours, string path, string id)
        {
            Stopwatch sw = new Stopwatch();
 
            Image SelectedPic = Image.FromFile(path + id + ".jpeg");
            bitMap = (Bitmap)SelectedPic;
            bitMapLbp = new Bitmap(SelectedPic.Width, SelectedPic.Height, PixelFormat.Format32bppRgb);

            sw.Restart();
            sw.Start();
          //  for (int x = radius; x < bitMap.Width - radius; x++){
                for (int y = radius; y < bitMap.Height - radius; y++)
                {
                   for (int x = radius; x < bitMap.Width - radius; x++)
                  { 
                        int value = middlepointValue(x, y, neighbours(x, y, radius, numNeighbours));

                        if (value > 255) {
                        value = value / 256;
                        }
                        Color color = Color.FromArgb(value,value,value);
                        bitMapLbp.SetPixel(x, y, color);
                   }
                }
          //  }
            sw.Stop();


            bitMapLbp.Save(Program.LPB_DATA_PATH + id + ".jpeg", ImageFormat.Jpeg);

            

            //long tiempo = (long)(sw.Elapsed.TotalMilliseconds);

             return HistogramToMatrix(SplitImage());
           
        }

        public void GrayScale(String path, string id)
        {
            Image SelectedPic = Image.FromFile(path + id + ".jpg");
            Bitmap bitmap = (Bitmap)SelectedPic;
            Color pixelColor;
            int[] nilaiLBP = new int[256];

            for (int i = 0; i < 256; i++)
                nilaiLBP[i] = 0;

            SelectedPic = new Bitmap(SelectedPic.Width, SelectedPic.Height);

            for (int y = 0; y < bitmap.Height; y++)
            {
                for (int x = 0; x < bitmap.Width; x++)
                {
                    pixelColor = bitmap.GetPixel(x, y);
                    int red = pixelColor.R;
                    int green = pixelColor.G;
                    int blue = pixelColor.B;
                    int rata = (int)(red + green + blue) / 3;
                    bitmap.SetPixel(x, y, Color.FromArgb(rata, rata, rata));
                }
            }

            bitmap.Save(Program.GRAY_DATA_PATH + id + ".jpeg", ImageFormat.Jpeg);
        }

        public List<Point> neighbours(int x, int y, int radius, int numNeighbours)
        {
            List<Point> neighbours = new List<Point>();

            
            for (int i = 0; i < numNeighbours; i++)
            {
                double t = (2 * Math.PI * i) / numNeighbours;

                double neighbourX = Math.Round(x + radius * Math.Cos(t), MidpointRounding.AwayFromZero);
                double neighbourY = Math.Round(y - radius * Math.Sin(t), MidpointRounding.AwayFromZero);

                Point nPoint = new Point((int)neighbourX, (int)neighbourY);
                neighbours.Add(nPoint);
                /*
                 double aux = Math.PI;
                 aux = numNeighbours;

                 aux = x;
                 aux = radius;
                 aux = radius;*/
            }



            return neighbours;
        }

        public int middlepointValue(int x, int y, List<Point> neighbours)
        {
            int sum = 0;
            int i = 0;

            
            foreach (Point point in neighbours)
            {
                
                 int aux = (bitMap.GetPixel(x, y).R.CompareTo(bitMap.GetPixel(point.X, point.Y).R) >= 0)? 1: 0 ;

                 if (aux == 1)
                 {
                     sum += (int)Math.Pow(2, i);
                 }

                 i++;
                /*
                                int aux = bitMap.GetPixel(x, y).R;
                aux = bitMap.GetPixel(x, y).R;

                sum = x;
                i++;*/
                
            }



            return sum ;
        }

       public double Distance(int[,] matrizA,int[,] matrizB)
        {
            int row = matrizA.GetLength(0);
            int col = matrizA.GetLength(1);

            double chi = 0;
            
            for (int i = 0; i < row;i++)
            {
                for (int j = 0; j < col; j++) 
                {
                    int suma  =  matrizA[i, j] + matrizB[i, j];
                    int resta = matrizA[i, j] - matrizB[i, j];

                    
                    if (resta == 0)
                    {
                        chi += 0;
                    }
                    else
                    {
                        chi += Math.Pow(resta, 2) / suma;
                    }
                    
                }
            }
            return chi;
        }

       public int[,] HistogramToMatrix(List<Dictionary<int, int>> imag)
        {
            int[,] matrix = new int[imag.Count, 256];
            int numDiccionario = 0;

            foreach (Dictionary<int, int>  dic in imag) 
            {
                for (int j = 0; j <= 255; j++)
                {
                    if (dic.ContainsKey(byte.Parse(j + "")))
                    {
                        matrix[numDiccionario, j] = dic[byte.Parse(j+"")];
                    }
                    else
                    {
                        matrix[numDiccionario, j] = 0;
                    }
                    
                }
                
                numDiccionario++;
            }
            return matrix;
        }
        public List<Dictionary<int, int>> SplitImage()
        {
            List<Dictionary<int, int>> listHistograms = new List<Dictionary<int, int>>();

            int numDic = 4;
            int z = 0;
            while (z < numDic)
            {
                Dictionary<int, int> histogram = new Dictionary<int, int>();

                listHistograms.Add(histogram);
                z++;
            }

            for (int y = 0; y < bitMapLbp.Height; y++)
            {
                for (int x = 0; x < bitMapLbp.Width; x++)
                {
                    if (x <= bitMapLbp.Height/2 && y <= bitMapLbp.Width/2)
                    {
                        Dictionary<int, int> histogram = listHistograms.ElementAt(0);
                        if (histogram.ContainsKey(bitMapLbp.GetPixel(x, y).R))
                        {
                            histogram[bitMapLbp.GetPixel(x, y).R]++;
                        }
                        else
                        {
                            histogram[bitMapLbp.GetPixel(x, y).R] = 1;
                        }
                    }
                    else if (x > bitMapLbp.Height / 2 && y <= bitMapLbp.Width / 2)
                    {
                        Dictionary<int, int> histogram = listHistograms.ElementAt(1);
                        if (histogram.ContainsKey(bitMapLbp.GetPixel(x, y).R))
                        {
                            histogram[bitMapLbp.GetPixel(x, y).R]++;
                        }
                        else
                        {
                            histogram[bitMapLbp.GetPixel(x, y).R] = 1;
                        }
                    }
                    else if (x <= bitMapLbp.Height / 2 && y > bitMapLbp.Width / 2)
                    {
                        Dictionary<int, int> histogram = listHistograms.ElementAt(2);
                        if (histogram.ContainsKey(bitMapLbp.GetPixel(x, y).R))
                        {
                            histogram[bitMapLbp.GetPixel(x, y).R]++;
                        }
                        else
                        {
                            histogram[bitMapLbp.GetPixel(x, y).R] = 1;
                        }
                    }
                    else if (x > bitMapLbp.Height/2 && y > bitMapLbp.Width/2) 
                    {
                        Dictionary<int, int> histogram = listHistograms.ElementAt(3);
                        if (histogram.ContainsKey(bitMapLbp.GetPixel(x, y).R))
                        {
                            histogram[bitMapLbp.GetPixel(x, y).R]++;
                        }
                        else
                        {
                            histogram[bitMapLbp.GetPixel(x, y).R] = 1;
                        }
                    }
                }
            }
            return listHistograms;
        }
        
        public double distancia()
        {
           // vect
            return 0.0;
        }
    }
}
