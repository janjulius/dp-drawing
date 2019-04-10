using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dp_drawing.Shape;

namespace dp_drawing.Patterns.Command
{
    class ResizeShape : Command
    {
        Shape.Shape shape;
        Point oldPoint;
        Point newPoint;
        Size sizeDifference;
        Size oldSize;
        Point oldPosition;
        ResizeMode resizeMode;

        public ResizeShape(Shape.Shape shape, Point oldPoint, Point newPoint, ResizeMode resizeMode)
        {
            this.shape = shape;
            this.oldPoint = oldPoint;
            this.newPoint = newPoint;
            this.sizeDifference = new Size((oldPoint.X - newPoint.X), (oldPoint.Y - newPoint.Y));
            this.resizeMode = resizeMode;
        }

        public override void Execute()
        {
            oldSize = shape.GetSize();
            oldPosition = shape.GetPosition();
            switch (resizeMode)
            {
                case ResizeMode.N:
                    shape.SetPosition(new Point(shape.GetPosition().X, shape.GetPosition().Y - sizeDifference.Height));
                    shape.SetSize(shape.GetSize().Width, shape.GetSize().Height + sizeDifference.Height);
                    break;

                case ResizeMode.S:
                    shape.SetSize(shape.GetSize().Width, shape.GetSize().Height - sizeDifference.Height);
                    break;

                case ResizeMode.E:
                    shape.SetSize(shape.GetSize().Width - sizeDifference.Width, shape.GetSize().Height);
                    break;

                case ResizeMode.W:
                    shape.SetPosition(new Point(shape.GetPosition().X - sizeDifference.Width, shape.GetPosition().Y));
                    shape.SetSize(shape.GetSize().Width + sizeDifference.Width, shape.GetSize().Height);
                    break;

                case ResizeMode.NW:
                    shape.SetPosition(new Point(shape.GetPosition().X - sizeDifference.Width, shape.GetPosition().Y - sizeDifference.Height));
                    shape.SetSize(shape.GetSize().Width + sizeDifference.Width, shape.GetSize().Height + sizeDifference.Height);
                    break;

                case ResizeMode.NE:
                    shape.SetPosition(new Point(shape.GetPosition().X, shape.GetPosition().Y - sizeDifference.Height));
                    shape.SetSize(shape.GetSize().Width - sizeDifference.Width, shape.GetSize().Height + sizeDifference.Height);
                    break;

                case ResizeMode.SW:
                    shape.SetPosition(new Point(shape.GetPosition().X - sizeDifference.Width, shape.GetPosition().Y));
                    shape.SetSize(shape.GetSize().Width + sizeDifference.Width, shape.GetSize().Height - sizeDifference.Height);
                    break;

                case ResizeMode.SE:
                    shape.SetSize(shape.GetSize().Width - sizeDifference.Width, shape.GetSize().Height - sizeDifference.Height);
                    break;

                default:
                    throw new NotImplementedException();
                    
            }
        }

        public override void Undo()
        {
            shape.SetSize(oldSize.Width, oldSize.Height);
            shape.SetPosition(oldPosition);
        }
    }
}
