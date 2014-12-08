using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Collections;
using System.Windows.Forms;

namespace fpjarmul
{
    static class Converter
    {
        public static ElementRGB imageToElementRGB(Image img)
        {
            ElementRGB raw = new ElementRGB();
            Bitmap inputImage = new Bitmap(img);
            Color pixelColor = new Color();
            raw.width = inputImage.Width;
            raw.height = inputImage.Height;

            raw.Red = new List<int>();
            raw.Green = new List<int>();
            raw.Blue = new List<int>();

            for (int x = 0; x < inputImage.Width; x++)
            {
                for (int y = 0; y < inputImage.Height; y++)
                {
                    pixelColor = inputImage.GetPixel(x, y);
                    raw.Red.Add(int.Parse(pixelColor.R.ToString()));
                    raw.Green.Add(int.Parse(pixelColor.G.ToString()));
                    raw.Blue.Add(int.Parse(pixelColor.B.ToString()));
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
                    Color imageColor = Color.FromArgb(data.Red.ElementAt(i), data.Green.ElementAt(i), data.Blue.ElementAt(i));
                    img.SetPixel(x, y, imageColor);
                    i++;
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

        public static int[] BinaryToNumeric(BitArray bits)
        {
            int[] data = new int[bits.Count];
            for (int i = 0; i < bits.Count; i++)
            {
                if (bits[i])
                {
                    data[i] = 1;
                }
                else
                {
                    data[i] = 0;
                }
            }

            return data;
        }

        public static BitArray NumericToBinary(int[] data)
        {
            BitArray bits = new BitArray(data.Length);
            for (int i = 0; i < data.Length; i++)
            {
                if (data[i]==1)
                {
                    bits.Set(i,true);
                }
                else
                {
                    bits.Set(i, false);
                }
            }

            return bits;
        }

        public static int[] base2ToBase3(int[] base2Array)
        {
            int[] base3Array = new int[base2Array.Length];
            for (int i = 0; i < base2Array.Length - 1; i += 2)
            {
                if (base2Array[i] == 1 && base2Array[i + 1] == 0)
                {
                    base3Array[i] = 0;
                    base3Array[i + 1] = 2;
                }
                else if (base2Array[i] == 1 && base2Array[i + 1] == 1)
                {
                    base3Array[i] = 1;
                    base3Array[i + 1] = 0;
                }
                else
                {
                    base3Array[i] = base2Array[i];
                    base3Array[i + 1] = base2Array[i+1];
                }
            }

            return base3Array;
        }

        public static int[] base3ToBase2(int[] base3Array)
        {
            int[] base2Array = new int[base3Array.Length];
            for (int i = 0; i < base3Array.Length - 1; i += 2)
            {
                if (base3Array[i] == 0 && base3Array[i + 1] == 2)
                {
                    base2Array[i] = 1;
                    base2Array[i + 1] = 0;
                }
                else if (base3Array[i] == 1 && base3Array[i + 1] == 0)
                {
                    base2Array[i] = 1;
                    base2Array[i + 1] = 1;
                }
                else
                {
                    base2Array[i] = base3Array[i];
                    base2Array[i + 1] = base3Array[i + 1];
                }
            }

            return base2Array;
        }
    }
}
