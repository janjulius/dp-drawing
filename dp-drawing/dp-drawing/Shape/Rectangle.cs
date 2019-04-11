using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace dp_drawing.Shape
{
    /// <summary>
    /// Represents the Rectangle shape
    /// </summary>
    sealed class Rectangle : Shape
    {
        public Rectangle(Color c, Point location, Size size, bool preview) : base(c, location, size, preview)
        {
            PictureBox = new PictureBox();
            InitializeShape();
        }

        public override void InitializeShape()
        {
            base.InitializeShape();
            Graphics g = PictureBox.CreateGraphics();
            g.FillRectangle(new SolidBrush(this.Color), new RectangleF(Position.X, Position.Y, size.Width, size.Height));
        }
    }
}
