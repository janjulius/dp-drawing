using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dp_drawing.Shape
{
    abstract class Shape
    {
        public List<Shape> children = new List<Shape>();

        public string Name { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public Point Position { get; set; }
        public Color Color { get; set; }

        
    }
}
