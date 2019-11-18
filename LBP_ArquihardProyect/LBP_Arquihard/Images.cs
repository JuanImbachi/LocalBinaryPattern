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
        public Images()
        {

        }
        public void GrayScale(){
            String path = "F:/Kuliah/Semester 6/Pengolahan Citra/DatasetLBP/11-1.jpeg";
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
                Console.WriteLine("Vecino #" + i);
                Console.WriteLine("  X =>  " + (x + radius * Math.Cos(t)));
                Console.WriteLine("  Y =>  " + (y - radius * Math.Sin(t)));

                double neighbourX = Math.Round(x + radius * Math.Cos(t), MidpointRounding.AwayFromZero);
                double neighbourY = Math.Round(y - radius * Math.Sin(t), MidpointRounding.AwayFromZero);

                Console.WriteLine("  newX =>  " + neighbourX);
                Console.WriteLine("  newY =>  " + neighbourY);

                Point nPoint = new Point((int)neighbourX, (int)neighbourY);

                neighbours.Add(nPoint);
            }

            return neighbours;
        }
    }
}
