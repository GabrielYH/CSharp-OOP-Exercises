using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeedForSpeed
{
    public class FamilyCar : Car
    {
        public FamilyCar(int horsePower, double fuel) : base(horsePower, fuel)
        {

        }
        public override double FuelConsumption { get => base.FuelConsumption; set => base.FuelConsumption = value; }

        public override void Drive(double km)
        {
            base.Drive(km);
        }
    }
}
