using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dp_drawing.Patterns.Composite
{
    /// <summary>
    /// Represents a component
    /// </summary>
    public abstract class Component<T>
    {
        public abstract void AddChild(T c);
        public abstract void RemoveChild(T c);
    }
}
