using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dp_drawing.Patterns.Command
{
    public abstract class Command
    {
        public abstract void Execute();

        public abstract void Undo();

        public virtual void Redo() => this.Execute();
    }
}
