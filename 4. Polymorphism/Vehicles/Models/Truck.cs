using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vehicles.Models
{
    public class Truck : Vehicle
    {
        private const double ConsumptionIncrement = 1.6;
        public Truck(double fuelQuantity, double fuelConsumption) : base(fuelQuantity, fuelConsumption, ConsumptionIncrement)
        {
        }

        public override void Refuel(double fuel)
        {
            base.Refuel(fuel*0.95);
        }
    }
}
