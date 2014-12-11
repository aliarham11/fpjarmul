using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;

namespace fpjarmul
{
    static class Steganography
    {
        static int[] temp;
        public static Image CreateStegoImage(ElementRGB rawData, byte[] secretData, Bitmap carrierImage)
        {
            Bitmap StegoImage = carrierImage;
            
            BitArray secretBits = Converter.ToBitArray(secretData);
            int[] numericBits = Converter.BinaryToNumeric(secretBits);
            int[] base3Data = Converter.base2ToBase3(numericBits);

            //Operasi Steganography di sini

            temp = new int[base3Data.Length + 1];
            int[] embededData = new int[base3Data.Length + 1];
            for (int i = 0; i < base3Data.Length; i++)
            {
                embededData[i] = base3Data[i];
                temp[i] = base3Data[i];
            }
            embededData[base3Data.Length] = 0;

            int index = 0;
            int ptr = 0;
            int f1,f2,f3;

            
            for (int x = 0; x < rawData.width; x++)
            {
                for (int y = 0; y < rawData.height; y++)
                {

                    if (ptr+2 <= base3Data.Length)
                    {
                        //Manipulasi Pixel//////////

                        //MessageBox.Show(rawData.Red.ElementAt(index) + " " + rawData.Green.ElementAt(index) + " " + rawData.Blue.ElementAt(index));

                        f1 = rawData.Red.ElementAt(index) % 3;
                        f2 = rawData.Green.ElementAt(index) % 3;
                        f3 = rawData.Blue.ElementAt(index) % 3;


                        ///////////////////////////////////////////////////////////////////////////////
                        if (f1 == embededData.ElementAt(ptr))
                        {
                            rawData.Red[index] = rawData.Red.ElementAt(index);
                        }
                        else
                        {
                            rawData.Red[index] = rawData.Red.ElementAt(index)+embededData.ElementAt(ptr)-f1;
                        }
                        /////////////////////////////////////////////////////////////////////////////////
                        /////////////////////////////////////////////////////////////////////////////////
                        if (f2 == embededData.ElementAt(ptr + 1))
                        {
                            rawData.Green[index] = rawData.Green.ElementAt(index);
                        }
                        else
                        {
                            rawData.Green[index] = rawData.Green.ElementAt(index)+embededData.ElementAt(ptr+1)-f2;
                        }
                        /////////////////////////////////////////////////////////////////////////////////
                        /////////////////////////////////////////////////////////////////////////////////
                        if (f3 == embededData.ElementAt(ptr + 2))
                        {
                            rawData.Blue[index] = rawData.Blue.ElementAt(index);
                        }
                        else
                        {
                            rawData.Blue[index] = rawData.Blue.ElementAt(index)+embededData.ElementAt(ptr+2)-f3;
                        }
                        /////////////////////////////////////////////////////////////////////////////////

                        //MessageBox.Show(f1+" "+embededData.ElementAt(ptr)+" "+f2+" "+embededData.ElementAt(ptr+1)+" "+f3+" "+embededData.ElementAt(ptr+2)+"\n"+rawData.Red.ElementAt(index)+" "+rawData.Green.ElementAt(index)+" "+rawData.Blue.ElementAt(index));

                        ptr += 3;

                        ////////////////////////////

                        Color imageColor;
                        //if (rawData.Blue.ElementAt(index) != -1 && rawData.Red.ElementAt(index)!= -1 && rawData.Green.ElementAt(index)!=-1)
                        {
                            imageColor = Color.FromArgb(rawData.Red.ElementAt(index), rawData.Green.ElementAt(index), rawData.Blue.ElementAt(index));
                            StegoImage.SetPixel(x, y, imageColor);
                        }

                        index++;
                    }
                }
            }
            rawData.StegoLength = index;
            
            return StegoImage;
        }

        public static SecretData ExtractSecretData(Image StegoImage, int stegoLength)
        {
            //Operasi extraksi secret data di sini
            List<int> embededData = new List<int>();
            int s1, s2, s3;
            ElementRGB rgbData = Converter.imageToElementRGB(StegoImage);
            for (int i = 0; i < stegoLength; i++)
            {
                s1 = rgbData.Red.ElementAt(i) % 3;
                s2 = rgbData.Green.ElementAt(i) % 3;
                s3 = rgbData.Blue.ElementAt(i) % 3;

                embededData.Add(s1);
                embededData.Add(s2);
                embededData.Add(s3);
            }

            //while ((embededData.Count % 8) != 0)
            embededData.RemoveAt(embededData.Count - 1);

            int[] base3Data = new int[embededData.Count];
            for (int i = 0; i < embededData.Count; i++)
            {
                base3Data[i] = embededData.ElementAt(i);
            }

            int[] base2Data = Converter.base3ToBase2(base3Data);
            BitArray binaryData = Converter.NumericToBinary(base2Data);
            byte[] byteData = Converter.ToByteArray(binaryData);
            SecretData secretData = Converter.byteToSecretData(byteData);

            return secretData;
        }
    }
}
