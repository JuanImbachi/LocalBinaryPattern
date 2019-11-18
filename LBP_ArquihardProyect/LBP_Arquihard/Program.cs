using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;
using System.Drawing.Imaging;
using LBP_Arquihard.Model;

namespace LBP_Arquihard
{
    class Program
    {
        static void Main(string[] args)
        {

            Images images = new Images();

            foreach(Point point in images.neighbours(3, 2, 2, 16))
            {
                Console.WriteLine(point.X + " - " + point.Y);
            }
            Console.ReadLine();
        }
            
    }
}
