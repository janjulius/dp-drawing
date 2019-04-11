using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dp_drawing.Shape;

namespace dp_drawing.Patterns.Command
{
    /// <summary>
    /// Represents the move shape command
    /// </summary>
    class MoveShape : Command
    {
        Shape.Shape shape;
        Point oldPoint;
        Point newPoint;
        Point oldLocation;
        Point undoLocation;

        public MoveShape(Shape.Shape shape, Point oldPoint, Point newPoint)
        {
            this.shape = shape;
            this.oldPoint = oldPoint;
            this.newPoint = newPoint;
        }

        public override void Execute()
        {
            oldLocation = shape.Position;
            Point p = GetNewPosition();
            shape.OffsetPosition(new Point(
                (p.X),
                (p.Y)));
        }

        private Point GetNewPosition()
        {
            return new Point(oldPoint.X - newPoint.X, oldPoint.Y - newPoint.Y);
        }

        public override void Redo()
        {
            var p = undoLocation;
            var s = shape.GetPosition();
            Point offset = new Point(s.X - p.X, s.Y - p.Y);
            shape.OffsetPosition(offset);
        }

        public override void Undo()
        {
            var p = oldLocation;
            var s = shape.GetPosition();
            undoLocation = shape.GetPosition();
            Point offset = new Point(s.X - p.X, s.Y - p.Y);
            shape.OffsetPosition(offset);
        }
    }
}
