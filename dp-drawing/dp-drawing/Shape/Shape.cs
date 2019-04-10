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
        private List<Shape> children = new List<Shape>();

        public Size size { get; set; }
        public Point Position { get; set; }
        public Color Color { get; set; }

        public PictureBox PictureBox { get; set; }

        public int Id { get; set; }

        private bool Preview = false;
        private bool isMouseDown = false;

        private bool moving = false;
        private bool resizing = false;
        ResizeMode resizeMode;

        private const int BorderSize = 10;

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

        internal void UpdateSize()
        {
            this.size = new Size(PictureBox.Size.Width, PictureBox.Size.Height);
        }

        internal void SetPosition(Point p)
        {
            PictureBox.Top = p.Y;
            PictureBox.Left = p.X;
            foreach(var child in children)
            {
                child.SetPosition(p);
            }
            PictureBox.BringToFront();
            UpdatePosition();
        }

        internal void OffsetPosition(Point p)
        {
            var leftOffset = PictureBox.Left - p.X;
            var rightOffset = PictureBox.Top - p.Y;
            PictureBox.Left = leftOffset;
            PictureBox.Top = rightOffset;
            foreach (var child in children)
            {
                child.OffsetPosition(p);
            }
            PictureBox.BringToFront();
            UpdatePosition();
        }

        internal void SetSize(int width, int height)
        {
            PictureBox.Size = new Size(width, height);
            foreach(var child in children)
            {
                child.IncreaseSize(width, height);
            }
            PictureBox.BringToFront();
            UpdateSize();
        }

        internal void IncreaseSize(int width, int height)
        {
            PictureBox.Size = new Size(
                PictureBox.Size.Width + width,
                PictureBox.Size.Height + height
                );
            foreach(var child in children)
            {
                child.IncreaseSize(width, height);
            }
            PictureBox.BringToFront();
            UpdateSize();
        }
        internal Point GetPosition()
        {
            return this.Position;
        }

        internal Size GetSize()
        {
            return this.size;
        }

        private void AddEvents()
        {
            PictureBox.MouseDown += setfocused_object;
            PictureBox.MouseUp += release_object;
            PictureBox.MouseHover += mouse_hover_event;
        }

        private void setfocused_object(object sender, EventArgs e)
        {
            mousePositions[0] = Cursor.Position;

            if (Control.ModifierKeys != Keys.Shift)
            {
                if (MouseIsOnEdge())
                {
                    moving = false;
                    resizing = true;
                    resizeMode = GetResizeMode();
                }
                else
                {
                    moving = true;
                    resizing = false;
                }
            }
            else
            {
                if (DrawingInstance.Instance.FocusedShape != null)
                {
                    DrawingInstance.Instance.FocusedShapes.Add(this);
                }
                else
                {
                    DrawingInstance.Instance.FocusedShape = this;
                }
            }
        }


        private void release_object(object sender, EventArgs e)
        {
            mousePositions[1] = Cursor.Position;
            if (moving)
            {
                var cmd = new MoveShape(this, mousePositions[0], mousePositions[1]);
                DrawingInstance.Instance.Commands.Push(cmd);
                cmd.Execute();
            }
            else if (resizing)
            {
                var cmd = new ResizeShape(this, mousePositions[0], mousePositions[1], resizeMode);
                DrawingInstance.Instance.Commands.Push(cmd);
                cmd.Execute();
            }
        }

        private void mouse_hover_event(object sender, EventArgs e)
        {
            if ((MouseIsOnBottomEdge() && MouseIsOnLeftEdge()) || (MouseIsOnRightEdge() && MouseIsOnTopEdge()))
            {
                MouseHelper.SetCursorStyle(Cursors.SizeNESW);
            }
            else if((MouseIsOnTopEdge() && MouseIsOnLeftEdge()) || (MouseIsOnBottomEdge() && MouseIsOnRightEdge()))
            {
                MouseHelper.SetCursorStyle(Cursors.SizeNWSE);
            }
            else if (MouseIsOnLeftEdge() || MouseIsOnRightEdge())
            {
                MouseHelper.SetCursorStyle(Cursors.SizeWE);
            }
            else if (MouseIsOnBottomEdge() || MouseIsOnTopEdge())
            {
                MouseHelper.SetCursorStyle(Cursors.SizeNS);
            }
            else
            {
                MouseHelper.SetCursorStyle(Cursors.SizeAll);
            }
        }

        private bool MouseIsOnLeftEdge()
        {
            return Math.Abs(GetMousePositionRelativeToPictureBox().X) <= BorderSize ? true : false;
        } 

        private bool MouseIsOnTopEdge()
        {
            return Math.Abs(GetMousePositionRelativeToPictureBox().Y) <= BorderSize ? true : false;
        }

        private bool MouseIsOnRightEdge()
        {
            return Math.Abs(GetMousePositionRelativeToPictureBox().X - this.size.Width) <= BorderSize ? true : false;
        }

        private bool MouseIsOnBottomEdge()
        {
            return Math.Abs(GetMousePositionRelativeToPictureBox().Y - this.size.Height) <= BorderSize ? true : false;
        }


        private bool MouseIsOnEdge()
        {
            return (MouseIsOnRightEdge() || MouseIsOnLeftEdge() || MouseIsOnBottomEdge() || MouseIsOnTopEdge());
        }

        private Point GetMousePositionRelativeToPictureBox()
        {
            return PictureBox.PointToClient(Cursor.Position);
        }

        private ResizeMode GetResizeMode()
        {
            if(MouseIsOnLeftEdge() && MouseIsOnBottomEdge())
            {
                return ResizeMode.SW;
            }
            else if(MouseIsOnLeftEdge() && MouseIsOnTopEdge())
            {
                return ResizeMode.NW;
            }
            else if(MouseIsOnRightEdge() && MouseIsOnTopEdge())
            {
                return ResizeMode.NE;
            }
            else if(MouseIsOnRightEdge() && MouseIsOnBottomEdge())
            {
                return ResizeMode.SE;
            }
            else if (MouseIsOnLeftEdge())
            {
                return ResizeMode.W;
            }
            else if (MouseIsOnRightEdge())
            {
                return ResizeMode.E;
            }
            else if (MouseIsOnTopEdge())
            {
                return ResizeMode.N;
            }
            else if (MouseIsOnBottomEdge())
            {
                return ResizeMode.S;
            }
            throw new NotImplementedException();
        }

        public void AddChild(Shape c)
        {
            this.children.Add(c);
        }

        public void RemoveChild(Shape c)
        {
            this.children.Remove(c);
        }

        public void DisplayDepth(int depth)
        {
            foreach (Shape child in children)
            {
                child.DisplayDepth(depth + 2);
            }

        }
    }
}
