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
            StreamWriter swm = new StreamWriter(@"F:\icesi\visualStudio\Distancias216.txt");
            Images images = new Images();

            int[,] matrixPro = null;
            for (int i = 1; i <= 4; i++)
            {
                for (int j = 1; j <= 2; j++)
                {
                    Console.WriteLine(i + "-" + j);
                    images.GrayScale(COLORED_DATA_PATH, "TNivel" + 3 + "/" + i + "-" + j);
                    int[,] matrix = images.lbp(2, 8, GRAY_DATA_PATH, "TNivel" + 3 + "/" + i + "-" + j);

                    if (i==1 && j==1)
                    {
                        swm.WriteLine("IMAGEN BASE Id." + i + j);
                        swm.Flush();
                        
                        matrixPro = matrix;
                    }
                    else
                    {
                        double distance = images.Distance(matrixPro, matrix);

                        Console.WriteLine( distance);
                        swm.WriteLine("Imagen No." + i + j);
                        swm.WriteLine(distance);
                        swm.Flush();
                    }

                    
                    
                    
                }
            }


            /*
                int[,] matrix;
                // Create an instance of StreamReader to read from a file.
                // The using statement also closes the StreamReader.
                using (StreamReader sr = new StreamReader(@"F:\icesi\visualStudio\matrix18\matrix42.txt"))
                {
                    string line;
                    // Read and display lines from the file until the end of 
                    // the file is reached.
                    line = sr.ReadLine();
                    String[] pos = line.Split(' ');

                     matrix = new int[int.Parse(pos[0]), int.Parse(pos[1])];

                    for (int m0 = 0; m0 < matrix.GetLength(0); m0++)
                    {
                        line = sr.ReadLine();
                        if (line != null)
                         {
                            String[] numbers = line.Split(' ');
                            for (int m1 = 0; m1 < matrix.GetLength(1); m1++)
                            {
                                matrix[m0, m1] = int.Parse(numbers[m1]);
                                if(matrix[m0,m1] != 0) 
                                {
                                Console.WriteLine(matrix[m0, m1]);
                                 }
                            }
                        }
                    }
                        sr.Close();
                }
                double distancia = Double.MaxValue;
                String idImag = "";

                for (int k = 1; k<=4; k++)
                {
                    for(int l =1; l<=2; l++)
                    {
                        if(k != 4 && l != 2)
                        {
                            
                            using (StreamReader sr = new StreamReader(@"F:\icesi\visualStudio\matrix18\matrix" + k + l + ".txt"))
                            {
                                string line;
                                // Read and display lines from the file until the end of 
                                // the file is reached.
                                line = sr.ReadLine();
                                Console.WriteLine(line);
                                String[] pos = line.Split(' ');

                                int [,] matrix2 = new int[int.Parse(pos[0]), int.Parse(pos[1])];

                                for (int m0 = 0; m0 < matrix2.GetLength(0); m0++)
                                {
                                    line = sr.ReadLine();
                                    if (line != null)
                                    {
                                        String[] numbers = line.Split(' ');
                                        for (int m1 = 0; m1 < matrix2.GetLength(1); m1++)
                                        {
                                            matrix2[m0, m1] = int.Parse(numbers[m1]);
                                        }
                                    }
                                }

                                
                                double newDistance = images.Distance(matrix, matrix2);
                                Console.WriteLine("IMAG "+k+l+" :::: "+newDistance);
                                if(newDistance < distancia)
                                {
                                    distancia = newDistance;
                                    idImag = k + "-" + l;
                                }
                                
                                sr.Close();
                            }
                        }
                        
                    }
                 }

                Console.WriteLine("DISTANCIA MENOR: " + distancia + " ID: " + idImag);
            */



            Console.WriteLine("TERMINÓ");
            Console.ReadLine();
           // sw.Close();
        }
            
    }
}
