using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.IO;

namespace ProyectoFinalArquiHard.model
{
    class FaceRecognition
    {

        public FaceRecognition()
        {
            Console.WriteLine("Obteniendo valores...");
            loadData();
            Console.WriteLine("Terminado");
        }

        public void loadData()
        {
            try
            {

                Bitmap imagen = new Bitmap("..\\..\\data\\imgs\\1.jpg");
                Bitmap grayImage = createGrayScaleBitmap(imagen);
                byte[,] imageBytes = createMatrix(grayImage);

            }
            catch (Exception e)
            {
                Console.WriteLine("Error " + e.Message);
            }
        }

        private byte[] getImageBytes(Bitmap image, ImageLockMode lockMode, out BitmapData bmpData)
        {
            bmpData = image.LockBits(new Rectangle(0, 0, image.Width, image.Height),
                                     lockMode, image.PixelFormat);
            byte[] imageBytes = new byte[bmpData.Stride * image.Height];
            Marshal.Copy(bmpData.Scan0, imageBytes, 0, imageBytes.Length);

            return imageBytes;
        }

        private Bitmap createGrayScaleBitmap(Bitmap source)
        {
            Bitmap target = new Bitmap(source.Width, source.Height, source.PixelFormat);
            BitmapData targetData, sourceData;

            byte[] sourceBytes = getImageBytes(source, ImageLockMode.ReadOnly, out sourceData);
            byte[] targetBytes = getImageBytes(target, ImageLockMode.ReadWrite, out targetData);
            //recorrer los pixeles
            for (int i = 0; i < sourceBytes.Length; i += 3)
            {
                //ignorar el padding, es decir solo procesar los bytes necesarios
                if ((i + 3) % (source.Width * 3) > 0)
                {
                    //Hallar tono gris
                    byte y = (byte)(sourceBytes[i + 2] * 0.3f
                                 + sourceBytes[i + 1] * 0.59f
                                 + sourceBytes[i] * 0.11f);
                    //Asignar tono gris a cada byte del pixel
                    targetBytes[i + 2] = targetBytes[i + 1] = targetBytes[i] = y;

                }
            }

            Marshal.Copy(targetBytes, 0, targetData.Scan0, targetBytes.Length);

            source.UnlockBits(sourceData);
            target.UnlockBits(targetData);

            return target;
        }
        private byte[,] createMatrix(Bitmap source)
        {
            Bitmap target = new Bitmap(source.Width, source.Height, source.PixelFormat);
            BitmapData sourceData;

            byte[,] ret = new byte[source.Width, source.Height];

            byte[] sourceBytes = getImageBytes(source, ImageLockMode.ReadOnly, out sourceData);
            int n = 0;
            int m = 0;
            for (int i = 0; i < sourceBytes.Length; i += 3)
            {
                if ((i + 3) % (source.Width * 3) > 0)
                {
                    byte y = (byte)(sourceBytes[i + 2] * 0.3f
                                 + sourceBytes[i + 1] * 0.59f
                                 + sourceBytes[i] * 0.11f);
                    ret[n, m] = y;
                    m += 1;
                    if (n == ret.GetLength(1) - 1)
                    {
                        m = 0;
                        n += 1;
                    }
                }
            }

            return ret;
        }
    }
}
