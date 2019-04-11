using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dp_drawing.Shape;

namespace dp_drawing.Patterns.Command
{
    class SelectShape : Command
    {
        Shape.Shape shape;

        public SelectShape(Shape.Shape shape)
        {
            this.shape = shape;
        }

        public override void Execute()
        {
            if (DrawingInstance.Instance.FocusedShape != null)
            {
                if (!DrawingInstance.Instance.FocusedShapes.Contains(shape)
                    && shape != DrawingInstance.Instance.FocusedShape)
                {
                    DrawingInstance.Instance.FocusedShapes.Add(shape);
                }
            }
            else
            {
                DrawingInstance.Instance.FocusedShape = shape;
            }
        }

        public override void Undo()
        {
            if(DrawingInstance.Instance.FocusedShapes.Count != 0)
            {
                DrawingInstance.Instance.FocusedShapes.Remove(shape);
            }
            else if(DrawingInstance.Instance.FocusedShape != null)
            {
                DrawingInstance.Instance.FocusedShape = null;
            }
        }
    }
}
