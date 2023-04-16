using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cars
{
    public class Tesla : ICar, IElectricCar
    {
        public Tesla(string model, string color, int baterries)
        {
            Model = model;
            Color = color;
            Battery = baterries;
        }
        public string Model { get; set; }
        public string Color { get; set; }
        public string Start { get; set; }
        public string Stop { get; set; }
        public int Battery { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new();
            sb.AppendLine($"{Color} {GetType().Name} {Model} with {Battery} Batteries");
            sb.AppendLine($"Engine start");
            sb.AppendLine($"Breaaak!");
            return sb.ToString().Trim();
        }
    }
}
