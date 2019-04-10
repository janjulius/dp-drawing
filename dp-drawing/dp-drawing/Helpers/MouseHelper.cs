using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace dp_drawing.Helpers
{
    class MouseHelper
    {
        public static void set_cursor_style_move(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.SizeAll;
        }

        public static void SetCursorStyle(Cursor style)
        {
            Cursor.Current = style;
        }
    }
}
