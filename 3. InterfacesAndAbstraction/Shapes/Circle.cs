using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shapes
{
    internal class Circle : IDrawable
    {
        private int radius;
        public Circle(int radius)
        {
            this.radius = radius;
        }
        
        public void Draw()
        {
            
        }
    }
}
