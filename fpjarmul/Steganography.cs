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
            
            BitArray secretBits = Converter.ToBitArray(secretData);
            int[] numericBits = Converter.BinaryToNumeric(secretBits);
            int[] base3Data = Converter.base2ToBase3(numericBits);

            //Operasi Steganography di sini

            int[] embededData = new int[base3Data.Length + 1];
            for (int i = 0; i < base3Data.Length; i++)
                embededData[i] = base3Data[i];
            embededData[base3Data.Length] = 0;

            int index = 0;
            for (int x = 0; x < rawData.width; x++)
            {
                for (int y = 0; y < rawData.height; y++)
                {
                    index++;

                    //Manipulasi Pixel//////////



                    ////////////////////////////

                    Color imageColor = Color.FromArgb(rawData.Red.ElementAt(index), rawData.Green.ElementAt(index), rawData.Blue.ElementAt(index));
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
