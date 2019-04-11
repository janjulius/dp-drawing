using dp_drawing.Ornaments;
using dp_drawing.Patterns.Command;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dp_drawing.Helpers
{
    /// <summary>
    /// Used for helping of writing and reading files
    /// </summary>
    public class FileHelper
    {
        public List<Shape.Shape> seen = new List<Shape.Shape>();
        Shape.Shape newShape;
        

        /// <summary>
        /// Writes a file to a constant path
        /// </summary>
        /// <returns></returns>
        public bool WriteFile()
        {
             
            StringBuilder sb = new StringBuilder();

            foreach (Shape.Shape s in DrawingInstance.Instance.Shapes)
            {
                if (!seen.Contains(s))
                {
                    sb.Append(GetFileString(s, new StringBuilder(), 0).ToString());
                }
            }

            Console.WriteLine(sb.ToString());
            string[] lines = sb.ToString().Split(Environment.NewLine.ToCharArray());
            File.WriteAllLines(Constants.path, lines);
            return true;
        }

        /// <summary>
        /// Recursive method to print out all the data of a shape for file saving
        /// </summary>
        /// <returns></returns>
        private StringBuilder GetFileString(Shape.Shape shape, StringBuilder data, int depth)
        {
            for(int i= 0; i < depth; i++)
            {
                data.Append("\t");
            }
            data.Append(shape.GetType().Name.ToString());
            data.Append(" ");
            data.Append($"{shape.GetPosition().X} {shape.GetPosition().Y}");
            data.Append(" ");
            data.Append($"{shape.GetSize().Width} {shape.GetSize().Height}");
            data.Append(" ");
            data.Append($"{shape.Color.R} {shape.Color.G} {shape.Color.B}");
            data.Append("\n");
            foreach (Ornament o in shape.GetOrnaments())
            {
                for (int i = 0; i < depth; i++)
                {
                    data.Append("\t");
                }
                data.Append("Ornament");
                data.Append(" ");
                data.Append(o.ornamentOrientation);
                data.Append(" ");
                data.Append($"\"{o.text}\"");
                data.Append("\n");
            }
            depth += 1;
            foreach (var child in shape.GetChildren())
            {
                seen.Add(child);
                GetFileString(child, data, depth);
            }
            return data;
        }

        /// <summary>
        /// reads file from constant path
        /// </summary>
        public void ReadFile()
        {
            string[] lines = File.ReadAllLines(Constants.path);
            Shape.Shape lastShape = null;
            int lastTabs = 0;

            foreach(var line in lines)
            {
                if(line.StartsWith("Rectangle") || line.StartsWith("Ellipse"))
                {
                    lastTabs = 0;
                    newShape = null;
                    lastShape = CreateShape(line);
                }
                if (line.StartsWith("Ornament"))
                {
                    lastTabs = 0;
                    CreateOrnament(line, lastShape);
                }
                if (line.StartsWith("\t"))
                {
                    int tabCount = line.Count(t => t == '\t');
                    
                    if (tabCount > lastTabs)
                    {
                        if (newShape == null)
                        {
                            if (IsShape(line))
                            {
                                lastShape.AddChild(IsShape(line) ? CreateShape(line) : null);
                                newShape = lastShape.GetChildren().Last();
                            } 
                        }
                        else
                        {
                            newShape.AddChild(IsShape(line) ? CreateShape(line) : null);
                            lastShape = newShape;
                            newShape = lastShape.GetChildren().Last();
                        }
                    }
                    if(tabCount == lastTabs)
                    {
                        if (IsShape(line))
                        {
                            lastShape.AddChild(IsShape(line) ? CreateShape(line) : null);
                            newShape = lastShape.GetChildren().Last();
                        }
                        else if (IsOrnament(line))
                        {
                            CreateOrnament(line, newShape ?? lastShape);
                        }
                    }
                    lastTabs = tabCount;
                }
            }
        }

        /// <summary>
        /// returns true if the line contains an ornament
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        private bool IsOrnament(string line)
        {
            return line.Trim('\t').StartsWith("Ornament");
        }

        /// <summary>
        /// returns true if the line contains an Ellipse or Rectangle
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        private bool IsShape(string line)
        {
            return line.Trim('\t').StartsWith("Rectangle") ||
                line.Trim('\t').StartsWith("Ellipse");
        }

        /// <summary>
        /// Creates a shape from the given string line
        /// </summary>
        /// <returns>Shape this method creates</returns>
        private Shape.Shape CreateShape(string line)
        {
            line.Trim('\t');
            string[] words = line.Split(' ');
            Color color = Color.FromArgb(255, Convert.ToByte(words[5]), Convert.ToByte(words[6]), Convert.ToByte(words[7]));

            Shapes shapetype = words[0].Trim('\t') == "Rectangle" ? Shapes.RECTANGLE : Shapes.ELLIPSE;
            var cmd = new DrawShape(
                shapetype,
                new System.Drawing.Point(
                    Convert.ToInt32(words[1])
                    , Convert.ToInt32(words[2])),
                new System.Drawing.Size(
                    Convert.ToInt32(words[3]),
                    Convert.ToInt32(words[4]))
                    ,
                false,
                color
                );
            cmd.Execute();
            return cmd.GetShape();
        }

        /// <summary>
        /// Creates an ornament from string line and adds it to given shape
        /// </summary>
        /// <returns>Ornament this method created</returns>
        private Ornament CreateOrnament(string line, Shape.Shape shape)
        {
            line.Trim('\t');
            string[] words = line.Split(' ');
            Ornament o = new Ornament(words[2].Trim('\"'), OrnamentHelper.GetOrnamentOrientationFromString(words[1]), shape);
            var cmd = new AddOrnament(
                shape,
                o);
            cmd.Execute();
            return o;
        }
    }
}
