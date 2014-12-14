using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace fpjarmul
{
    [Serializable()]
    class PacketData : ISerializable
    {
        public Image stegoImage;
        public Image realImage;
        public String secretText;
        public Image secretImage;
        public int stegoLength;
        public PacketData()
        { 
        }

        public PacketData(Image image, int stegoLength)
        {
            // TODO: Complete member initialization
            this.stegoImage = image;
            this.stegoLength = stegoLength;
        }

        public PacketData(SerializationInfo info, StreamingContext ctxt)
        {
            stegoImage = (Image)info.GetValue("StegoImage",typeof(Image));
            realImage = (Image)info.GetValue("RealImage", typeof(Image));
            secretImage = (Image)info.GetValue("SecretImage", typeof(Image));
            stegoLength = (int)info.GetValue("StegoLength",typeof(int));
            secretText = (String)info.GetValue("SecretText",typeof(String));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            info.AddValue("StegoImage", stegoImage);
            info.AddValue("StegoLength", stegoLength);
            info.AddValue("SecretText", secretText);
            info.AddValue("SecretImage", secretImage);
            info.AddValue("RealImage", realImage);
        }
    }
}
