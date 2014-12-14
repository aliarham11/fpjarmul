using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace fpjarmul
{
    [Serializable]
    class SecretData : ISerializable
    {
        public String SecretText;
        public Image SecretImage;

        public SecretData()
        {
 
        }

        public SecretData(SerializationInfo info, StreamingContext ctxt)
        {
            SecretImage = (Image)info.GetValue("SecretImage",typeof(Image));
            SecretText = (String)info.GetValue("SecretText",typeof(String));
            
        }

        public void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            info.AddValue("SecretImage", SecretImage);
            info.AddValue("SecretText", SecretText);
        }
    }
}
