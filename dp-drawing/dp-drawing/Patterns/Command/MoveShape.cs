using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dp_drawing.Shape;

namespace dp_drawing.Patterns.Command
{
    class MoveShape : Command
    {
        Shape.Shape shape;
        Point oldPoint;
        Point newPoint;
        Point oldLocation;

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
            shape.PictureBox.Top -= p.Y;
            shape.PictureBox.Left -= p.X;
            shape.PictureBox.BringToFront();
            shape.UpdatePosition();
            Console.WriteLine($@"Moved from {oldLocation} to {p}");
        }

        private Point GetNewPosition()
        {
            return new Point(oldPoint.X - newPoint.X, oldPoint.Y - newPoint.Y);
        }

        public override void Redo()
        {
            this.Execute();
        }

        public override void Undo()
        {
            var p  = oldLocation;
            shape.PictureBox.Top = p.Y;
            shape.PictureBox.Left = p.X;
            shape.PictureBox.BringToFront();
            Console.WriteLine($@"Moving back to to {shape.PictureBox.Location}");
        }
    }
}
