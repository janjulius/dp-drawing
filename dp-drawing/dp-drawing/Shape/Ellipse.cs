using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace dp_drawing.Shape
{
    sealed class Ellipse : Shape
    {
        public Ellipse(Color c, Point location, Size size, bool preview) : base(c, location, size, preview)
        {
            PictureBox = new EllipsePictureBox();
            InitializeShape();
        }
        
        public override void InitializeShape()
        {
            base.InitializeShape();
            Graphics g = PictureBox.CreateGraphics();
            //g.FillEllipse(new SolidBrush(this.Color), Location.X, Location.Y, Width, Height);
        }
    }
}
