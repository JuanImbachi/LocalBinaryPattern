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
            for (int n = 1; n <= 4; n++)
            {
                for (int i = 1; i <= 4; i++)
                {
                    for (int j = 1; j <= 2; j++)
                    {
                        images.GrayScale(COLORED_DATA_PATH + "TNivel" + n + "/" , i + "-" + j);
                    }
                }
            }

        }
            
    }
}
