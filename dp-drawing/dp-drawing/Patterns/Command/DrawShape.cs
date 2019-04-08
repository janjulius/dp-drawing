using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace dp_drawing.Patterns.Command
{
    class DrawShape : Command
    {
        Shape.Shape shape = null;
        PictureBox pb = null;

        Shapes SelectedShape = Shapes.NONE;
        Point location;
        Size size;
        bool preview;
        Color selectedColor;

        public DrawShape(Shapes shape, Point location, Size size, bool preview, Color color)
        {
            this.SelectedShape = shape;
            this.location = location;
            this.size = size;
            this.preview = preview;
            this.selectedColor = color;
            switch (shape)
            {
                case Shapes.RECTANGLE:
                    this.shape = new Shape.Rectangle(selectedColor, location, size, preview);
                    break;

                case Shapes.ELLIPSE:
                    this.shape = new Shape.Ellipse(selectedColor, location, size, preview);
                    break;
            }
        }

        public override void Execute()
        {
            try
            {
                pb = shape.PictureBox;
                Form1.ActiveForm.Controls.Add(pb);
                if (!preview)
                {
                    Form1.ActiveForm.Controls.Add(shape);
                    DrawingInstance.Instance.ShapeStartIndex++;
                }
                pb.BringToFront();
            }
            catch { }
            Form1.ActiveForm.Invalidate();
            //g.FillRectangle(sb, relativePoint.X, relativePoint.Y, 20, 20);
        }

        public override void Redo()
        {
            this.Execute();
        }

        public override void Undo()
        {
            Form1.ActiveForm.Controls.Remove(shape);
            Form1.ActiveForm.Controls.Remove(pb);
        }


    }
}
