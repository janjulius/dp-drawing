using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace dp_drawing.Ornaments
{
    /// <summary>
    /// represents an ornament
    /// </summary>
    public class Ornament
    {
        public string text = "";
        public Shape.Shape parent;
        public OrnamentOrientation ornamentOrientation;
        public TextBox tb;

        public Ornament(string text, OrnamentOrientation oo, Shape.Shape shape)
        {
            tb = new TextBox();
            tb.Text = text;
            this.text = text;
            tb.Enabled = false;
            tb.TextAlign = HorizontalAlignment.Center;
            parent = shape;
            ornamentOrientation = oo;
        }

        public void UpdatePosition()
        {
            tb.Location = CalculatePosition();
            tb.BringToFront();
            Console.WriteLine($"Set position to {tb.Location}");
        }

        private Point CalculatePosition()
        {
            var ppos = parent.GetPosition();
            var psize = parent.GetSize();
            var c = (psize.Width / 2) - tb.Size.Width / 2;
            var x = ppos.X + c;
            switch (ornamentOrientation)
            {
                case OrnamentOrientation.Top:
                    return new Point(x, ppos.Y - (tb.Size.Height / 2));
                case OrnamentOrientation.Right:
                    return new Point((ppos.X + psize.Width) - (tb.Size.Width / 2), ppos.Y + (psize.Height / 2) - (tb.Size.Height / 2));
                case OrnamentOrientation.Bottom:
                    return new Point(x, (ppos.Y + psize.Height +(tb.Size.Height / 2)) - tb.Size.Height);
                case OrnamentOrientation.Left:
                    return new Point(ppos.X - (tb.Size.Width / 2), ppos.Y + (psize.Height / 2) - (tb.Size.Height / 2));
            }
            return parent.Position; 
        }

        public void SetParent(Shape.Shape s)
        {
            this.parent = s;
        }

        public override string ToString()
        {
            return $"Ornament OR {ornamentOrientation}, text: {text}, parent:{parent}";
        }
    }
}
