using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Models.Interfaces;

namespace Test.Models
{
    public class Car : Vehicle
    {
        private const double fuelIncrement = 0.9;
        public Car(double fuelQuantity, double fuelConsumption, double tankCapacity) : base(fuelQuantity, fuelConsumption, fuelIncrement, tankCapacity)
        {

        }

    }
}
