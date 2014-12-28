using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fpjarmul
{
    [Serializable]
    class ElementRGB
    {
        public List<int> Red { get; set; }
        public List<int> Green { get; set; }
        public List<int> Blue { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public int StegoLength { get; set; }
        
    }
}
