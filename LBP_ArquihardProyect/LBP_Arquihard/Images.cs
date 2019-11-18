using System;
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

        public void lbp(int radius, int numNeighbours)
        {
            String path = "F:/Kuliah/Semester 6/Pengolahan Citra/DatasetLBP/file.png";
            Image SelectedPic = Image.FromFile(path);
            Bitmap bitmap = (Bitmap)SelectedPic;

            bitMap = bitmap;
            bitMapLbp = bitmap;
            for (int y = radius; y < bitMap.Height - radius; y++)
            {
                for (int x = radius; x < bitMap.Width -radius; x++)
                {
                    int value = middlepointValue(x, y, neighbours(x, y, radius, numNeighbours));
                    bitMapLbp.SetPixel(x,y,Color.FromArgb(value,value,value));
                }
            }

            bitmap.Save("F:/Kuliah/Semester 6/Pengolahan Citra/DatasetLBP/file2.png", ImageFormat.Png);

        }

        public void GrayScale(String path){
            Image SelectedPic = Image.FromFile(path);
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

            bitmap.Save("F:/Kuliah/Semester 6/Pengolahan Citra/DatasetLBP/file.png", ImageFormat.Png);
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

        public void splitImage(int width, int height)
        {

            int d1 = bitMapLbp.Width / width;
            int d2 = bitMapLbp.Height / height;

        }
    }
}
