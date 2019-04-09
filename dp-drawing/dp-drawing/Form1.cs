﻿using dp_drawing.Patterns.Command;
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

        Shape.Shape focusedShape = null;

        List<Control> ImportantControls = new List<Control>();

        Point[] measurePoint = new Point[2];
        Size shapeSize = new Size();

        bool MouseDown = false;

        private int shapeStartIndex = 0;
        
        Stack<Command> commandStack = new Stack<Command>();
        Stack<Command> rubbishStack = new Stack<Command>();

        Stack<Command> redoStack = new Stack<Command>();

        public Form1()
        {
            InitializeComponent();
            //ShapeStartIndex = Controls.Count;
            DrawingInstance.Instance.ShapeStartIndex = Controls.Count;
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
            //Console.WriteLine("Color picker opened");
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

           //if (DrawingInstance.Instance.Shapes.Count != 0)
           //{
           //    focusedShape = GetShapeAtPoint(measurePoint[0]);
           //}
            if (DrawingInstance.Instance.FocusedShape != null)
            {
                Console.WriteLine($@"selected shape with id: {DrawingInstance.Instance.FocusedShape.Id} is a {DrawingInstance.Instance.FocusedShape.GetType().Name}");
                DrawingInstance.Instance.FocusedShape = null;
            }
        }

        //private Shape.Shape GetShapeAtPoint(Point point)
        //{
        //    foreach(Shape.Shape shape in DrawingInstance.Instance.Shapes)
        //    {
        //        var x = shape.Location.X;
        //        var y = shape.Location.Y;
        //        var x2 = shape.Location.X + shape.size.Width;
        //        var y2 = shape.Location.Y + shape.size.Height;
        //        if((point.X > x && point.X < x2)
        //            && (point.Y > y && point.Y < y2))
        //        {
        //            return shape;
        //        }
        //    }
        //    return null;
        //}

        private void canvas_MouseUp(object sender, MouseEventArgs e)
        {
            MouseDown = false;
            measurePoint[1] = GetCursorRelativePoint();

            var s = GetShapeSize(measurePoint[0], measurePoint[1]);
            //Console.WriteLine($"{measurePoint[0]}, {measurePoint[1]}, {s}");
            

            if (measurePoint[0].X < measurePoint[1].X
                && measurePoint[0].Y > measurePoint[1].Y) //from left bottom to right top
            {
                ExecuteCommand(new DrawShape(SelectedShape, new Point(measurePoint[0].X, measurePoint[1].Y), s, false, selectedColor));
            }
            else if (measurePoint[0].X > measurePoint[1].X
               && measurePoint[0].Y < measurePoint[1].Y) //from right top to left bottom
            {
                ExecuteCommand(new DrawShape(SelectedShape, new Point(measurePoint[1].X, measurePoint[0].Y), s, false, selectedColor));
            }
            else if (measurePoint[0].X > measurePoint[1].X
                && measurePoint[0].Y > measurePoint[1].Y) //from right bottom to left top
            {
                ExecuteCommand(new DrawShape(SelectedShape, measurePoint[1], s, false, selectedColor));
            }
            else //from left top to right bottom
            {
                ExecuteCommand(new DrawShape(SelectedShape, measurePoint[0], s, false, selectedColor));
            }
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
                ExecuteCommand(new DrawShape(SelectedShape, measurePoint[0], size, true, selectedColor));
            }
        }

        private Size GetShapeSize(Point a, Point b)
        {
            var sub = Point.Subtract(a, new Size(b));
            return new Size(Math.Abs(sub.X), Math.Abs(sub.Y));
        }

        private bool ExecuteCommand(Command command)
        {
            redoStack.Clear();
            if(command is DrawShape)
            {
                if (((DrawShape)command).IsPreview())
                {
                    if(rubbishStack.Count > 0)
                    {
                        var cmd = rubbishStack.Pop(); cmd.Undo();
                    }
                    rubbishStack.Push(command);
                    command.Execute();
                    return true;
                }
                else
                {
                    for(int i = rubbishStack.Count; i > 0; i--)
                    {
                        var cmd = rubbishStack.Pop();
                        cmd.Undo();
                    }
                }
            }
            commandStack.Push(command);
            command.Execute();
            return true;
        }

        private void UndoButton_Click(object sender, EventArgs e)
        {
            Command cmd = commandStack.Pop();
            redoStack.Push(cmd);
            cmd.Undo();
        }

        private void RedoButton_Click(object sender, EventArgs e)
        {
            Command cmd = redoStack.Pop();
            commandStack.Push(cmd);
            cmd.Redo();
        }
    }
}
