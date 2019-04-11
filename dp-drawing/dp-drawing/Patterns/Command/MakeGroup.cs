using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dp_drawing.Shape;

namespace dp_drawing.Patterns.Command
{
    /// <summary>
    /// Represents the make group command
    /// </summary>
    class MakeGroup : Command
    {
        Shape.Shape parent;
        List<Shape.Shape> children;

        private Shape.Shape oldFocusedShape;
        private List<Shape.Shape> oldFocusedShapes;

        public MakeGroup(Shape.Shape parent, List<Shape.Shape> children)
        {
            this.parent = parent;
            this.children = children;
        }

        public override void Execute()
        {
            oldFocusedShape = DrawingInstance.Instance.FocusedShape;
            oldFocusedShapes = new List<Shape.Shape>();
            foreach (var shap in DrawingInstance.Instance.FocusedShapes)
            {
                parent.AddChild(shap);
                oldFocusedShapes.Add(shap);
            }
            DrawingInstance.Instance.FocusedShape = null;
            DrawingInstance.Instance.FocusedShapes.Clear();
        }

        public override void Undo()
        {
            this.parent.ClearChildren();
            DrawingInstance.Instance.FocusedShape = oldFocusedShape;
            DrawingInstance.Instance.FocusedShapes = oldFocusedShapes;
        }
    }
}
