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
        public List<Shape.Shape> Shapes = new List<Shape.Shape>();

        private Shape.Shape focusedShape = null;
        public Shape.Shape FocusedShape {
            get
            {
                return focusedShape;
            }
            set
            {
                focusedShape = value;

            }
        }

        private int curId = -1;
        public int GenerateShapeId
        {
            get
            {
                curId++;
                return curId;
            }
        }

        private DrawingInstance()
        {

        }
        public static DrawingInstance Instance
        {
            get
            {
                if(instance != null)
                {
                    return instance;
                }
                instance = new DrawingInstance();
                return instance;
            }
        }
        
    }
}
