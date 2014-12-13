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
            stegoLength = (int)info.GetValue("StegoLength",typeof(int));
            
 
        }

        public void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            info.AddValue("StegoImage", stegoImage);
            info.AddValue("StegoLength", stegoLength);
        }
    }
}
