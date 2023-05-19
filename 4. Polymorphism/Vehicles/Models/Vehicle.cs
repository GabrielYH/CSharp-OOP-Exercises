using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehicles.Exeptions;
using Vehicles.Models.Interfaces;

namespace Vehicles.Models
{
    public class Vehicle : IVehicle
    {
        public Vehicle(double fuelQuantity, double fuelConsumption, double consumptionIncrement)
        {
            FuelQuantity = fuelQuantity;
            FuelConsumption = fuelConsumption + consumptionIncrement;
        }
        public double FuelQuantity { get; private set; }

        public double FuelConsumption { get; private set; }

        public string Drive(double distance)
        {
            if (distance * FuelConsumption > FuelQuantity )
            {
                throw new InsufficientFuelException(string.Format(ExceptionMessages.InsufficientFuelMessage, this.GetType().Name));
            }
            FuelQuantity -= distance * FuelConsumption;
            return $"{this.GetType().Name} travelled {distance} km";
        }

        public virtual void Refuel(double fuel)
        {
            FuelConsumption += fuel;
        }

        public override string ToString()
        {
            return $"{this.GetType().Name}: {FuelQuantity:f2}";
        }
    }
}
