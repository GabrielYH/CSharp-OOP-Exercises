using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Models
{
    public class Bus : Vehicle
    {
        private const double fuelIncrement = 1.4;
        public Bus(double fuelQuantity, double fuelConsumption, double tankCapacity) : base(fuelQuantity, fuelConsumption, fuelIncrement, tankCapacity)
        {
        }

        public string DriveEmpty(double distance)
        {
            double fuelNeeded = distance * (FuelConsumption - fuelIncrement);
            if (fuelNeeded <= FuelQuantity)
            {
                FuelQuantity -= fuelNeeded;
                return $"{this.GetType().Name} travelled {distance} km";
            }
            return $"{this.GetType().Name} needs refueling";
        }
    }
}
