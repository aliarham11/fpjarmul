using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace fpjarmul
{
    [Serializable]
    class SecretData
    {
        public String SecretText { get; set; }
        public Image SecretImage { get; set; }
    }
}
