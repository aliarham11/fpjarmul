using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace fpjarmul
{
    class Converter
    {
        public ElementRGB imageToElementRGB(Image img)
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

        public Image elementRGBToImage(ElementRGB data)
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

        public List<byte> secretDataToByte(SecretData data)
        {
            return null;
        }

        public SecretData byteToSecretData(List<Byte> data)
        {
            return null;
        }
    }
}
