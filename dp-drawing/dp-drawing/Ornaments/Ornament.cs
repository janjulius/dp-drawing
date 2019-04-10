using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace dp_drawing.Ornaments
{
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
            parent = shape;
            ornamentOrientation = oo;
        }

        public void UpdatePosition()
        {
            tb.Location = parent.Position;
            tb.BringToFront();
            Console.WriteLine($"Set position to {tb.Location}");
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
