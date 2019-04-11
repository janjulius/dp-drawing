using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace dp_drawing.Helpers
{
    /// <summary>
    /// Help with mouse methods
    /// </summary>
    class MouseHelper
    {
        /// <summary>
        /// Sets mouse style to given cursor pointer
        /// </summary>
        public static void SetCursorStyle(Cursor style)
        {
            Cursor.Current = style;
        }
    }
}
