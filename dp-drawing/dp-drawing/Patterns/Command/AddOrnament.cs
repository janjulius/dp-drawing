using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dp_drawing.Ornaments;
using dp_drawing.Shape;

namespace dp_drawing.Patterns.Command
{
    class AddOrnament : Command
    {
        Shape.Shape shape;
        Ornament ornament;

        public AddOrnament(Shape.Shape shape, Ornament ornament)
        {
            this.shape = shape;
            this.ornament = ornament;
        }

        public override void Execute()
        {
            Form1.ActiveForm.Controls.Add(ornament.tb);
            ornament.SetParent(shape);
            shape.AddOrnament(ornament);
            ornament.UpdatePosition();
        }

        public override void Undo()
        {
            Form1.ActiveForm.Controls.Remove(ornament.tb);
            ornament.SetParent(null);
            shape.RemoveOrnament(ornament);
        }
    }
}
