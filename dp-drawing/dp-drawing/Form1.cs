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
        Color selectedColor = Color.White;



        public Form1()
        {
            InitializeComponent();
            g = canvas.CreateGraphics();
        }

        private void canvas_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Canvas click");
            SolidBrush sb = new SolidBrush(selectedColor);
             this.Cursor = new Cursor(Cursor.Current.Handle); //todo
            g.FillRectangle(sb, Cursor.Position.X, Cursor.Position.Y, 20, 20);
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
    }
}
