using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassBoxData
{
    public class Box
    {
        private double length;
        private double width;
        private double height;
        public Box(double length, double width, double height)
        {
            Length = length;
            Width = width;
            Height = height;
        }
        public double Length 
        {
            get { return length; }
            private set
            {
                if (value <= 0) throw new ArgumentException(string.Format(ExeptionMessages.BoxParameterCannotBeZeroOrNegative, nameof(this.Length)));
                length = value;
            }
        }
        public double Width
        {
            get { return width; }
            private set
            {
                if (value <= 0) throw new ArgumentException(string.Format(ExeptionMessages.BoxParameterCannotBeZeroOrNegative, nameof(this.Width)));
                width = value;
            }
        }

        public double Height
        {
            get { return height; }
            private set
            {
                if (value <= 0) throw new ArgumentException(string.Format(ExeptionMessages.BoxParameterCannotBeZeroOrNegative, nameof(this.Height))); 
                height = value;
            }
        }

        public double SurfaceArea()
        {
            double surfaceArea = 2*(Length*Width)+ 2*(Length*Height)+ 2*(height*Width); ;
            return surfaceArea;
        }
        public double LateralSurfaceArea()
        {
            double lateralSurfaceArea = 2 * (Length * Height) + 2 * (Width * Height);
            return lateralSurfaceArea;
        }

        public double Volume() => Length * Width * Height;
        public override string ToString()
        {
            StringBuilder sb = new();
            sb.AppendLine($"Surface Area - {SurfaceArea():f2}");
            sb.AppendLine($"Lateral Surface Area - {LateralSurfaceArea():f2}");
            sb.AppendLine($"Volume - {Volume():f2}");
            return sb.ToString().Trim();
        }


    }
}
