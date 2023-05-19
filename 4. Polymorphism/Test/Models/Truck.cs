using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Models.Interfaces;

namespace Test.Models
{
    public class Truck : Vehicle
    {
        private const double fuelIncrement = 1.6;
        public Truck(double fuelQuantity, double fuelConsumption, double tankCapacity) : base(fuelQuantity, fuelConsumption, fuelIncrement, tankCapacity)
        {

        }
        public override void Refuel(double amount)
        {
            if (amount + FuelQuantity > TankCapacity)
            {
                throw new ArgumentException($"Cannot fit {amount} fuel in the tank");
            }

            base.Refuel(amount * 0.95);
        }
    }
}
