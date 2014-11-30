using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Collections;

namespace fpjarmul
{
    class Steganography
    {
        public Image CreateStegoImage(ElementRGB rawData, byte[] secretData)
        {
            Bitmap StegoImage = new Bitmap(rawData.width, rawData.height, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            BitArray secretBits = new BitArray(secretData);

            //Operasi Steganography di sini

            int i = 0;
            for (int x = 0; x < rawData.width; x++)
            {
                for (int y = 0; y < rawData.height; y++)
                {
                    i++;

                    //Manipulasi Pixel//////////



                    ////////////////////////////

                    Color imageColor = Color.FromArgb(rawData.Red.ElementAt(i), rawData.Green.ElementAt(i), rawData.Blue.ElementAt(i));
                    StegoImage.SetPixel(x, y, imageColor);
                }
            }

            return StegoImage;
        }

        public SecretData ExtractSecretData(Image StegoImage)
        {
            //Operasi extraksi secret data di sini

            return null;
        }
    }
}
