using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Shapes
{
    public class Rectangle : Shape
    {
        public Rectangle(int height, int width)
        {
            Height = height;
            Width = width;
        }
        public int Height { get; private set; }
        public int Width { get; private set; }
        public override double CalculateArea()
        {
            return Width * Height;
        }

        public override double CalculatePerimeter()
        {
            return 2 * Width + 2 * Height;
        }


    }
}
