using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shapes
{
    public class Rectangle : IDrawable
    {
        private int width;
        private int height;
        public Rectangle(int width, int height)
        {
            this.width = width;
            this.height = height;
        }
        public void Draw()
        {
            
        }
    }
}
