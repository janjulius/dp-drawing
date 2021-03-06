﻿using dp_drawing.Helpers;
using dp_drawing.Patterns.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace dp_drawing
{
    public partial class Form1 : Form
    {
        Graphics g = null;
        Color selectedColor = Color.MediumPurple;
        Shapes SelectedShape = Shapes.RECTANGLE;

        Shape.Shape focusedShape = null;

        List<Control> ImportantControls = new List<Control>();

        Point[] measurePoint = new Point[2];
        Size shapeSize = new Size();

        bool MouseDown = false;

        private int shapeStartIndex = 0;

        string ornamentText = "Yeet";

        public Form1()
        {
            InitializeComponent();
            DrawingInstance.Instance.ShapeStartIndex = Controls.Count;
            g = canvas.CreateGraphics();
            textBox1.BackColor = selectedColor;
            this.Text = Constants.name;
        }

        private void canvas_Load(object sender, EventArgs e)
        {
            this.Click += canvas_Click;
        }

        private void canvas_Click(object sender, EventArgs e)
        {
            DrawingInstance.Instance.FocusedShape = null;
            DrawingInstance.Instance.FocusedShapes.Clear();
        }

        private void colorpicker_Click(object sender, EventArgs e)
        {
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

        private void canvas_MouseDown(object sender, MouseEventArgs e)
        {
            MouseDown = true;
            measurePoint[0] = GetCursorRelativePoint();
            
            if (DrawingInstance.Instance.FocusedShape != null)
            {
                //Console.WriteLine($@"selected shape with id: {DrawingInstance.Instance.FocusedShape.Id} is a {DrawingInstance.Instance.FocusedShape.GetType().Name}");
                DrawingInstance.Instance.FocusedShapes.Clear();
                DrawingInstance.Instance.FocusedShape = null;
            }
        }

        private void canvas_MouseUp(object sender, MouseEventArgs e)
        {
            MouseDown = false;
            measurePoint[1] = GetCursorRelativePoint();

            var s = GetShapeSize(measurePoint[0], measurePoint[1]);
            

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

        /// <summary>
        /// returns point of cursor on the form
        /// </summary>
        /// <returns></returns>
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
                measurePoint[1] = GetCursorRelativePoint();

                var s = GetShapeSize(measurePoint[0], measurePoint[1]);


                if (measurePoint[0].X < measurePoint[1].X
                    && measurePoint[0].Y > measurePoint[1].Y) //from left bottom to right top
                {
                    ExecuteCommand(new DrawShape(SelectedShape, new Point(measurePoint[0].X, measurePoint[1].Y), s, true, selectedColor));
                }
                else if (measurePoint[0].X > measurePoint[1].X
                   && measurePoint[0].Y < measurePoint[1].Y) //from right top to left bottom
                {
                    ExecuteCommand(new DrawShape(SelectedShape, new Point(measurePoint[1].X, measurePoint[0].Y), s, true, selectedColor));
                }
                else if (measurePoint[0].X > measurePoint[1].X
                    && measurePoint[0].Y > measurePoint[1].Y) //from right bottom to left top
                {
                    ExecuteCommand(new DrawShape(SelectedShape, measurePoint[1], s, true, selectedColor));
                }
                else //from left top to right bottom
                {
                    ExecuteCommand(new DrawShape(SelectedShape, measurePoint[0], s, true, selectedColor));
                }
            }
        }
        
        private Size GetShapeSize(Point a, Point b)
        {
            var sub = Point.Subtract(a, new Size(b));
            return new Size(Math.Abs(sub.X), Math.Abs(sub.Y));
        }

        /// <summary>
        /// Executes a command
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        private bool ExecuteCommand(Command command)
        {
            DrawingInstance.Instance.RedoStack.Clear();
            if(command is DrawShape)
            {
                if (((DrawShape)command).IsPreview())
                {
                    if (DrawingInstance.Instance.RubbishStack.Count > 0)
                    {
                        var cmd = DrawingInstance.Instance.RubbishStack.Pop(); cmd.Undo();
                    }
                    DrawingInstance.Instance.RubbishStack.Push(command);
                    command.Execute();
                    return true;
                }
                else
                {
                    for(int i = DrawingInstance.Instance.RubbishStack.Count; i > 0; i--)
                    {
                        var cmd = DrawingInstance.Instance.RubbishStack.Pop();
                        cmd.Undo();
                    }
                }
            }
            DrawingInstance.Instance.Commands.Push(command);
            command.Execute();
            return true;
        }

        private void UndoButton_Click(object sender, EventArgs e)
        {
            if (DrawingInstance.Instance.Commands.Count != 0)
            {
                Command cmd = DrawingInstance.Instance.Commands.Pop();
                DrawingInstance.Instance.RedoStack.Push(cmd);
                cmd.Undo();
            }
        }

        private void RedoButton_Click(object sender, EventArgs e)
        {
            if (DrawingInstance.Instance.RedoStack.Count != 0)
            {
                Command cmd = DrawingInstance.Instance.RedoStack.Pop();
                DrawingInstance.Instance.Commands.Push(cmd);
                cmd.Redo();
            }
        }

        private void MakeGroupButton_Click(object sender, EventArgs e)
        {
            if (DrawingInstance.Instance.FocusedShape != null || DrawingInstance.Instance.FocusedShapes.Count != 0)
            {
                ExecuteCommand(new MakeGroup(DrawingInstance.Instance.FocusedShape, DrawingInstance.Instance.FocusedShapes));
                MessageBox.Show("Group created");
                return;
            }
            MessageBox.Show("You need at least 2 shapes to make a group you fucking retard", "lmao?");
        }

        private void AddOrnament_Click(object sender, EventArgs e)
        {
            if(DrawingInstance.Instance.FocusedShape != null)
            {
               ExecuteCommand(new AddOrnament(
                    DrawingInstance.Instance.FocusedShape,
                    new Ornaments.Ornament(
                       ornamentText,
                        OrnamentHelper.GetOrnamentOrientationFromString(OrnamentComboBox.Text),
                        DrawingInstance.Instance.FocusedShape)));
                return;
            }
            MessageBox.Show("You need to select a shape");
        }

        private void OrnamentTextBox_TextChanged(object sender, EventArgs e)
        {
            TextBox objTextBox = (TextBox)sender;
            ornamentText = objTextBox.Text;
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            var fh = new FileHelper();
            fh.WriteFile();
        }

        private void LoadButton_Click(object sender, EventArgs e)
        {
            var fh = new FileHelper();
            fh.ReadFile();
        }
    }
}
