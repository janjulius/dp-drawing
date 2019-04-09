using dp_drawing.Helpers;
using dp_drawing.Patterns.Command;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace dp_drawing.Shape
{
    public abstract class Shape
    {
        public List<Shape> children = new List<Shape>();

        public Size size { get; set; }
        public Point Position { get; set; }
        public Color Color { get; set; }

        public PictureBox PictureBox { get; set; }

        public int Id { get; set; }

        private bool Preview = false;
        private bool isMouseDown = false;

        private Point[] mousePositions = new Point[3];

        
        /// <summary>
        /// Create a shape
        /// </summary>
        /// <param name="c">Color for the shape</param>
        /// <param name="location">Location of the shape as System.Drawing.Point</param>
        /// <param name="size">Size of the shape as System.Drawing.Size</param>
        protected Shape(Color c, Point location, Size size, bool preview)
        {
            PictureBox = new PictureBox();
            Color = c;
            Position = location;
            this.size = size;
            Preview = preview;
        }

        public virtual void InitializeShape()
        {
            this.PictureBox.Location = this.Position;
            this.PictureBox.Size = new Size(size.Width, size.Height);
            var col = Color.FromArgb(Preview ? Constants.PreviewTransparency : 255, Color.R, Color.G, Color.B);
            this.PictureBox.BackColor = col;

            if (!Preview)
                AddEvents();
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

        internal void UpdatePosition()
        {
            this.Position = new Point(PictureBox.Location.X, PictureBox.Location.Y);
        }


        private void AddEvents()
        {
            PictureBox.MouseDown += setfocused_object;
            PictureBox.MouseUp += release_object;
            PictureBox.MouseHover += MouseHelper.set_cursor_style_move;
        }

        private void setfocused_object(object sender, EventArgs e)
        {
            mousePositions[0] = Cursor.Position;
            DrawingInstance.Instance.FocusedShape = this;
        }

        private void release_object(object sender, EventArgs e)
        {
            mousePositions[1] = Cursor.Position;
            var cmd = new MoveShape(this, mousePositions[0], mousePositions[1]);
            DrawingInstance.Instance.Commands.Push(cmd);
            cmd.Execute();
        }
    }
}
