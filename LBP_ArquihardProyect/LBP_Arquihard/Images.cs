using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
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

        public double lbp(int radius, int numNeighbours, string path, string id)
        {
            Stopwatch sw = new Stopwatch();

            sw.Restart();
            sw.Start();

            Image SelectedPic = Image.FromFile(path + id + ".jpeg");
            bitMap = (Bitmap)SelectedPic;
            bitMapLbp = new Bitmap(SelectedPic.Width, SelectedPic.Height, PixelFormat.Format32bppRgb);

            sw.Restart();
            sw.Start();

            for (int y = radius; y < bitMap.Height - radius; y++)
            {
                for (int x = radius; x < bitMap.Width - radius; x++)
                {
                    int value = middlepointValue(x, y, neighbours(x, y, radius, numNeighbours)) % 255;
                    Color color = Color.FromArgb(value, value, value);
                    bitMapLbp.SetPixel(x, y, color);
                }
            }
            sw.Stop();

            bitMapLbp.Save(Program.LPB_DATA_PATH + id + ".jpeg", ImageFormat.Jpeg);

            

            long tiempo = (long)(sw.Elapsed.TotalMilliseconds);

            return tiempo;
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
            }

            return sum ;
        }

        public List<Dictionary<int, int>> SplitImage(int width, int height)
        {

            int d1 = bitMapLbp.Width / width;
            int d2 = bitMapLbp.Height / height;
            List<Dictionary<int, int>> listHistograms = new List<Dictionary<int, int>>();

            int numDic = d1 * d2;
            int z = 0;
            while (z < numDic)
            {
                Dictionary<int, int> histogram = new Dictionary<int, int>();

                listHistograms.Add(histogram);
                z++;
            }

            int aux = -2;

            for (int j = 0; j < bitMap.Height; j++)
            {
                if (j % d2 == 0)
                {
                    aux++;
                }

                for (int i = 0; i < bitMap.Width; i++)
                {
                    if (i % d1 == 0)
                    {
                        aux++;
                    }
                    Dictionary<int, int> histogram = listHistograms.ElementAt(aux);

                    if (histogram.ContainsKey(bitMapLbp.GetPixel(i, j).R))
                    {
                        histogram[bitMapLbp.GetPixel(i, j).R]++;
                    }
                    else
                    {
                        histogram[bitMapLbp.GetPixel(i, j).R] = 1;
                    }
                }

            }

            return listHistograms;
        }
    }
}
