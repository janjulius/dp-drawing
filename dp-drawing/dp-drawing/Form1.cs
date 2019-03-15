using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace dp_drawing
{
    public partial class Form1 : Form
    {
        Graphics g = null;
        Color selectedColor = Color.Red;
        Shapes SelectedShape = Shapes.RECTANGLE;

        List<Shape.Shape> shapes = new List<Shape.Shape>();
        List<Control> ImportantControls = new List<Control>();

        Point[] measurePoint = new Point[2];
        Size shapeSize = new Size();

        bool MouseDown = false;

        int ShapeStartIndex = 0;
       

        public Form1()
        {
            InitializeComponent();
            ShapeStartIndex = Controls.Count;
            g = canvas.CreateGraphics();
            textBox1.BackColor = selectedColor;
        }

        private void canvas_Load(object sender, EventArgs e)
        {
            this.Paint += canvas_Paint;
            this.Click += canvas_Click;
        }

        private void canvas_Click(object sender, EventArgs e)
        {
           
        }

        private void colorpicker_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Color picker opened");
            ColorDialog MyDialog = new ColorDialog();
            MyDialog.AllowFullOpen = true;
            MyDialog.ShowHelp = true;
            MyDialog.Color = textBox1.ForeColor;
            if (MyDialog.ShowDialog() == DialogResult.OK)
            {
                textBox1.BackColor = MyDialog.Color;
                selectedColor = MyDialog.Color;
            }
        }

        private void ellipseButton_Click(object sender, EventArgs e)
        {
            SelectedShape = Shapes.ELLIPSE;
        }

        private void rectButton_Click(object sender, EventArgs e)
        {
            SelectedShape = Shapes.RECTANGLE;
        }

        private void canvas_Paint(object sender, PaintEventArgs e)
        {
            if (SelectedShape == Shapes.ELLIPSE)
            {

                Graphics G = e.Graphics;
                    using (Pen myPen = new Pen(System.Drawing.Color.MediumPurple, 5))
                    {
                    Point relativePoint =
               (this.PointToClient(
                   new Point(
                       Cursor.Position.X,
                       Cursor.Position.Y)));
                    Rectangle myRectangle = new Rectangle(new Point(relativePoint.X, relativePoint.Y), new Size(0, 0));
                            myRectangle.Inflate(new Size(20, 20));
                            G.DrawEllipse(myPen, myRectangle);
                        
                    }
                
            }
        }

        private void canvas_MouseDown(object sender, MouseEventArgs e)
        {
            MouseDown = true;
            measurePoint[0] = GetCursorRelativePoint();
            
        }

        private void canvas_MouseUp(object sender, MouseEventArgs e)
        {
            MouseDown = false;
            measurePoint[1] = GetCursorRelativePoint();

            var s = GetShapeSize(measurePoint[0], measurePoint[1]);
            Console.WriteLine($"{measurePoint[0]}, {measurePoint[1]}, {s}");

            RewriteControls();

            if (measurePoint[0].X < measurePoint[1].X
                && measurePoint[0].Y > measurePoint[1].Y) //from left bottom to right top
            {
                DrawShape(new Point(measurePoint[0].X, measurePoint[1].Y), s, false);
            }
            else if (measurePoint[0].X > measurePoint[1].X
               && measurePoint[0].Y < measurePoint[1].Y) //from right top to left bottom
            {
                DrawShape(new Point(measurePoint[1].X, measurePoint[0].Y), s, false);
            }
            else if (measurePoint[0].X > measurePoint[1].X
                && measurePoint[0].Y > measurePoint[1].Y) //from right bottom to left top
            {
                DrawShape(measurePoint[1], s,false);
            }
            else //from left top to right bottom
            {
                DrawShape(measurePoint[0], s,false);
            }
        }


        private void DrawShape(Point location, Size size, bool preview)
        {
            Console.WriteLine($@"Selected shape: {SelectedShape}");
            
            Shape.Shape shape = null;
            PictureBox pb = null;
            switch (SelectedShape)
            {
                case Shapes.RECTANGLE:
                    shape = new Shape.Rectangle(selectedColor, location, size, preview);
                    break;
                case Shapes.ELLIPSE:
                    shape = new Shape.Ellipse(selectedColor, location, size, preview);
                    break;
                case Shapes.NONE:
                    throw new NullReferenceException("Shape not found");
            }
            try
            {
                pb = shape.PictureBox;
                this.Controls.Add(pb);
                if (!preview) { 
                    shapes.Add(shape);
                    this.Controls.Add(shape);
                    ShapeStartIndex++;
                }
                pb.BringToFront();
            }
            catch { }
            this.Invalidate();
            //g.FillRectangle(sb, relativePoint.X, relativePoint.Y, 20, 20);
        }

        private void RewriteControls()
        {
            while (Controls.Count != ShapeStartIndex)
            {
                Controls.RemoveAt(0);
            }
            this.Invalidate();
            g.Clear(Color.Silver);
        }

        private Point GetCursorRelativePoint()
        {
            return
                (this.PointToClient(
                    new Point(
                        Cursor.Position.X,
                        Cursor.Position.Y)));
        }

        private void canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (MouseDown)
            {
                var size = GetShapeSize(measurePoint[0], GetCursorRelativePoint());
                DrawShape(measurePoint[0], size, true);
                Console.WriteLine($"m: {measurePoint[0]} , {size}");
            }
        }

        private Size GetShapeSize(Point a, Point b)
        {
            var sub = Point.Subtract(a, new Size(b));
            return new Size(Math.Abs(sub.X), Math.Abs(sub.Y));
        }
    }
}
