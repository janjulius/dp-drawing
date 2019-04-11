using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dp_drawing.Patterns.Command
{
    /// <summary>
    /// represents a command
    /// </summary>
    public abstract class Command
    {
        public abstract void Execute();

        public abstract void Undo();

        public virtual void Redo() => this.Execute();
    }
}
