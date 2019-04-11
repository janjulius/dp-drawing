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
    /// Represents the resize shape command
    /// </summary>
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

        //TODO make size not negative ever
        public override void Execute()
        {
            oldSize = shape.GetSize();
            oldPosition = shape.GetPosition();
            switch (resizeMode)
            {
                case ResizeMode.N:
                    shape.OffsetPosition(new Point(0,sizeDifference.Height));
                    shape.IncreaseSize(0, sizeDifference.Height);
                    break;

                case ResizeMode.S:
                    shape.IncreaseSize(0, -sizeDifference.Height);
                    break;

                case ResizeMode.E:
                    shape.IncreaseSize(-sizeDifference.Width, 0);
                    break;

                case ResizeMode.W:
                    shape.OffsetPosition(new Point(sizeDifference.Width, 0));
                    shape.IncreaseSize(sizeDifference.Width, 0);
                    break;

                case ResizeMode.NW:
                    shape.OffsetPosition(new Point(sizeDifference.Width, sizeDifference.Height));
                    shape.IncreaseSize(sizeDifference.Width,sizeDifference.Height);
                    break;

                case ResizeMode.NE:
                    shape.OffsetPosition(new Point(0, sizeDifference.Height));
                    shape.IncreaseSize(-sizeDifference.Width, sizeDifference.Height);
                    break;

                case ResizeMode.SW:
                    shape.OffsetPosition(new Point(sizeDifference.Width, 0));
                    shape.IncreaseSize(sizeDifference.Width, -sizeDifference.Height);
                    break;

                case ResizeMode.SE:
                    shape.IncreaseSize(-sizeDifference.Width, -sizeDifference.Height);
                    break;

                default:
                    throw new NotImplementedException();
                    
            }
        }

        public override void Undo()
        {
            shape.IncreaseSize(oldSize.Width - shape.size.Width, oldSize.Height - shape.size.Height);
            shape.OffsetPosition(new Point(shape.Position.X-oldPosition.X, shape.Position.Y-oldPosition.Y));
        }
    }
}
