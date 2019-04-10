using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dp_drawing.Shape;

namespace dp_drawing.Patterns.Command
{
    class SelectShape : Command
    {
        Shape.Shape shape;

        public SelectShape(Shape.Shape shape)
        {
            this.shape = shape;
        }

        public override void Execute()
        {
            throw new NotImplementedException();
        }

        public override void Undo()
        {
            throw new NotImplementedException();
        }
    }
}
