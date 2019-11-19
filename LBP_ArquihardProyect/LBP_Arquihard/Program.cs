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
        public const String COLORED_DATA_PATH = "C:/Users/juand/OneDrive/Documentos/GitHub/LocalBinaryPattern/LBP_ArquihardProyect/LBP_Arquihard/Resources/ColoredDataset/";
        public const String GRAY_DATA_PATH = "C:/Users/juand/OneDrive/Documentos/GitHub/LocalBinaryPattern/LBP_ArquihardProyect/LBP_Arquihard/Resources/GrayDataset/";
        public const String LPB_DATA_PATH = "C:/Users/juand/OneDrive/Documentos/GitHub/LocalBinaryPattern/LBP_ArquihardProyect/LBP_Arquihard/Resources/LBPDataset/";
        static void Main(string[] args)
        {

            Images images = new Images();
            for(int i = 1; i <= 8; i++)
            {
                images.GrayScale(COLORED_DATA_PATH, i);
                images.lbp(2, 8, GRAY_DATA_PATH, i);
            }

        }
            
    }
}
