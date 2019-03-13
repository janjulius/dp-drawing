using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace dp_drawing
{
    public partial class Form1 : Form
    {
        Graphics g = null;
        Color selectedColor = Color.Red;
        Shapes SelectedShape = Shapes.RECTANGLE;

        List<PictureBox> shapes = new List<PictureBox>();


        public Form1()
        {
            InitializeComponent();
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
            Console.WriteLine($@"Selected shape: {SelectedShape}");
            Point relativePoint =
                (this.PointToClient(
                    new Point(
                        Cursor.Position.X,
                        Cursor.Position.Y)));
            Shape.Shape shape = null;
            PictureBox pb = null;
            switch (SelectedShape)
            {
                case Shapes.RECTANGLE:
                    shape = new Shape.Rectangle(selectedColor, new Point(relativePoint.X, relativePoint.Y), new Size(20, 20));
                    break;
                case Shapes.ELLIPSE:
                    shape = new Shape.Ellipse(selectedColor, new Point(relativePoint.X, relativePoint.Y), new Size(20, 20));
                    break;
                case Shapes.NONE:
                    throw new NullReferenceException("Shape not found");
            }
            try
            {
                pb = shape.PictureBox;
                this.Controls.Add(pb);
                shapes.Add(shape);
                pb.BringToFront();
            }catch{}
            this.Invalidate();
            //g.FillRectangle(sb, relativePoint.X, relativePoint.Y, 20, 20);
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
    }
}
