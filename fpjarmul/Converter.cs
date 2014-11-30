using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Collections;

namespace fpjarmul
{
    static class Converter
    {
        public static ElementRGB imageToElementRGB(Image img)
        {
            ElementRGB raw = new ElementRGB();
            Bitmap inputImage = new Bitmap(img);
            raw.width = inputImage.Width;
            raw.height = inputImage.Height;
            for (int x = 0; x < inputImage.Width; x++)
            {
                for (int y = 0; y < inputImage.Height; y++)
                {
                    Color pixelColor = inputImage.GetPixel(x, y);
                    raw.Red.Add(pixelColor.R);
                    raw.Green.Add(pixelColor.G);
                    raw.Blue.Add(pixelColor.B);
                }
            }

            return raw;
        }

        public static Image elementRGBToImage(ElementRGB data)
        {
            Bitmap img = new Bitmap(data.width, data.height, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            int i=0;
            for (int x = 0; x < data.width; x++)
            {
                for (int y = 0; y < data.height; y++)
                {
                    i++;
                    Color imageColor = Color.FromArgb(data.Red.ElementAt(i), data.Green.ElementAt(i), data.Blue.ElementAt(i));
                    img.SetPixel(x, y, imageColor);
                }
            }

            return img;
        }

        public static byte[] secretDataToByte(SecretData data)
        {
            if (data == null)
                return null;
            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream ms = new MemoryStream();
            bf.Serialize(ms, data);
            return ms.ToArray();
        }

        public static SecretData byteToSecretData(byte[] array)
        {
            MemoryStream memStream = new MemoryStream();
            BinaryFormatter binForm = new BinaryFormatter();
            memStream.Write(array, 0, array.Length);
            memStream.Seek(0, SeekOrigin.Begin);
            SecretData data = (SecretData)binForm.Deserialize(memStream);
            return data;
        }

        public static BitArray ToBitArray(byte[] bytes)
        {
            var bits = new BitArray(bytes);

            return bits;
        }

        public static byte[] ToByteArray(this BitArray bits)
        {
            int numBytes = bits.Count / 8;
            if (bits.Count % 8 != 0) numBytes++;

            byte[] bytes = new byte[numBytes];
            int byteIndex = 0, bitIndex = 0;

            for (int i = 0; i < bits.Count; i++)
            {
                if (bits[i])
                    bytes[byteIndex] |= (byte)(1 << (7 - bitIndex));

                bitIndex++;
                if (bitIndex == 8)
                {
                    bitIndex = 0;
                    byteIndex++;
                }
            }

            return bytes;
        }
    }
}
