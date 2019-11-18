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

            images.lbp(2,8);
            Console.WriteLine("YA");
            images.SplitImage(2, 2);
            Console.WriteLine("YA2");
            Console.ReadLine();

        }
            
    }
}
