using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace dp_drawing.Shape
{
    abstract class Shape : PictureBox
    {
        public List<Shape> children = new List<Shape>();

        public string Name { get; set; }
        public Size size { get; set; }
        public Point Position { get; set; }
        public Color Color { get; set; }

        public PictureBox PictureBox { get; private set; }

        private bool isMouseDown = false;

        /// <summary>
        /// Create a shape
        /// </summary>
        /// <param name="c">Color for the shape</param>
        /// <param name="location">Location of the shape as System.Drawing.Point</param>
        /// <param name="size">Size of the shape as System.Drawing.Size</param>
        protected Shape(Color c, Point location, Size size)
        {
            PictureBox = new PictureBox();
            Color = c;
            Position = location;
            Size = size;
        }

        public abstract void PaintShapeEvent(object sender, PaintEventArgs e);

        public virtual void InitializeShape()
        {
            this.PictureBox.Location = this.Position;
            //this.PictureBox.Name = "";
            this.PictureBox.Size = new Size(this.Width, this.Height);
            this.PictureBox.BackColor = this.Color;
        }

        public virtual void MouseDownEvent(object sender, MouseEventArgs e)
        {
            Console.WriteLine("mousedown");
            isMouseDown = true;
        }
        public virtual void MouseMoveEvent(object sender, MouseEventArgs e) { }

        public virtual void MouseUpEvent(object sender, MouseEventArgs e)
        {
            Console.WriteLine("mouseup");
            isMouseDown = false;
        }
    }
}
