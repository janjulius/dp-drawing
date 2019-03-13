using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace dp_drawing.Shape
{
    sealed class Rectangle : Shape
    {
        public Rectangle(Color c, Point location, Size size) : base(c, location, size)
        {
            InitializeShape();
        }
        
        public override void PaintShapeEvent(object sender, PaintEventArgs e)
        {
            //e.Graphics.FillRectangle(new SolidBrush(color), rect);
        }

        public override void InitializeShape()
        {
            base.InitializeShape();
            Graphics g = PictureBox.CreateGraphics();
            g.FillRectangle(new SolidBrush(this.Color), new RectangleF(Position.X, Position.Y, Width, Height));
        }
    }
}
