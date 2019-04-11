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

        Shapes SelectedShape = Shapes.NONE;
        Point location;
        Size size;
        Color selectedColor;
        bool preview;

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
            this.shape.Id = DrawingInstance.Instance.GenerateShapeId;
        }

        public override void Execute()
        {
            try
            {
                Form1.ActiveForm.Controls.Add(shape.PictureBox);
                if (!preview)
                {
                    DrawingInstance.Instance.ShapeStartIndex++;
                    DrawingInstance.Instance.Shapes.Add(this.shape);
                    Console.WriteLine($@"{location} + {size} = {location+size}");
                }
                shape.PictureBox.BringToFront();
            }
            catch { }
            Form1.ActiveForm.Invalidate();
            //g.FillRectangle(sb, relativePoint.X, relativePoint.Y, 20, 20);
        }

        public override void Undo()
        {
            Form1.ActiveForm.Controls.Remove(shape.PictureBox);

            if (!preview) 
                DrawingInstance.Instance.Shapes.Remove(this.shape);
        }

        public Shape.Shape GetShape()
        {
            return this.shape;
        }

        public bool IsPreview()
        {
            return preview;
        }
    }
}
