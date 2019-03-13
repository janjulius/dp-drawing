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
        public Ellipse(Color c, Point location, Size size) : base(c, location, size)
        {
            PictureBox.Paint += new System.Windows.Forms.PaintEventHandler(this.PaintShapeEvent);
            InitializeShape();
        }
        
        public override void InitializeShape()
        {
            
            base.InitializeShape();
            Graphics g = PictureBox.CreateGraphics();
            //g.FillEllipse(new SolidBrush(this.Color), Location.X, Location.Y, Width, Height);
            
        }

        public override void PaintShapeEvent(object sender, PaintEventArgs e)
        {
            Graphics G = e.Graphics;
            System.Drawing.Rectangle myRectangle = new System.Drawing.Rectangle(new Point(0, 0), new Size(0, 0));
            myRectangle.Inflate(new Size(125, 100));
            using (Pen myPen = new Pen(System.Drawing.Color.Green, 5))
            {
                G.DrawEllipse(myPen, myRectangle);
            }
        }
    }
}
