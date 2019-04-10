using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dp_drawing.Helpers
{
    class OrnamentHelper
    {
        internal static OrnamentOrientation GetOrnamentOrientationFromString(string or)
        {
            return 
                or == "Left" ? OrnamentOrientation.Left 
                : or == "Right" ? OrnamentOrientation.Right 
                : or == "Top" ? OrnamentOrientation.Top 
                : or == "Bottom" ? OrnamentOrientation.Bottom : OrnamentOrientation.Top;
        }
    }
}
