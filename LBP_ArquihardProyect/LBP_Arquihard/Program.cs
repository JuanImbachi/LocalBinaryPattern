using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using LBP_Arquihard.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LBP_Arquihard
{
    class Program
    {
        public const String COLORED_DATA_PATH = "C:/Users/LENOVO/Source/Repos/LocalBinaryPattern/LBP_ArquihardProyect/LBP_Arquihard/Resources/ColoredDataset/";
        public const String GRAY_DATA_PATH = "C:/Users/LENOVO/Source/Repos/LocalBinaryPattern/LBP_ArquihardProyect/LBP_Arquihard/Resources/GrayDataset/";
        public const String LPB_DATA_PATH = "C:/Users/LENOVO/Source/Repos/LocalBinaryPattern/LBP_ArquihardProyect/LBP_Arquihard/Resources/LBPDataset/";
        static void Main(string[] args)
        {
            StreamWriter sw = new StreamWriter(@"F:\icesi\visualStudio\radio2vecino16ji.txt");
            

            Images images = new Images();
            
            for(int n = 1; n <= 4; n++){
                sw.WriteLine("nivel: "+n);
                sw.Flush();

                for (int r= 1; r<=5;r++)
                {
                  
                           // images.GrayScale(COLORED_DATA_PATH , "TNivel" + n + "/" + i + "-" + j);
                           
                            Double time=images.lbp(2, 16, GRAY_DATA_PATH,"TNivel" + n + "/" + 4 + "-" + 2);
                           
                            Console.WriteLine("TNivel" + n + "/" + 4 + "-" + 2+" ::::: "+time);
                           
                            sw.WriteLine(time);
                            sw.Flush();
                   
                }
            }
            sw.Close();
        }
            
    }
}
