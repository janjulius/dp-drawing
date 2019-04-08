using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dp_drawing
{
    public sealed class DrawingInstance
    {
        private static DrawingInstance instance = null;

        public int ShapeStartIndex = 0;

        private DrawingInstance()
        {

        }
        public static DrawingInstance Instance
        {
            get
            {
                return instance ?? new DrawingInstance();
            }
        }
    }
}
