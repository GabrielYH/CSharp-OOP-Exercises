using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cars
{
    public class Seat : ICar
    {
        public Seat(string model, string color)
        {
            Model = model;
            Color = color;
        }
        public string Model { get; set; }
        public string Color { get; set; }
        public string Start { get; set; }
        public string Stop { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new();
            sb.AppendLine($"{Color} {GetType().Name} {Model}");
            sb.AppendLine($"Engine start");
            sb.AppendLine($"Breaaak!");
            return sb.ToString().Trim();
        }
    }
}
