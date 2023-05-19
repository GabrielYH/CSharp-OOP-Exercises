using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Models.Interfaces;

namespace Test.Models
{
    public abstract class Vehicle : IVehicle
    {
        public Vehicle(double fuelQuantity, double fuelConsumption, double fuelIncrement, double tankCapacity)
        {
            FuelQuantity = fuelQuantity;
            FuelConsumption = fuelConsumption + fuelIncrement;
            TankCapacity = tankCapacity;
            if (FuelQuantity > TankCapacity)
            {
                FuelQuantity = 0;
            }
        }
        public double TankCapacity { get; protected set; }

        public double FuelQuantity { get; protected set; }

        public double FuelConsumption { get; protected set; }

        public string Drive(double distance)
        {
            double fuelNeeded = distance * FuelConsumption;
            if (fuelNeeded <= FuelQuantity)
            {
                FuelQuantity -= fuelNeeded;
                return $"{this.GetType().Name} travelled {distance} km";
            }
            return $"{this.GetType().Name} needs refueling";
        }

        public virtual void Refuel(double fuel)
        {
            if (fuel <= 0)
            {
                Console.WriteLine("Fuel must be a positive number");
                return;
            }
            if (fuel + FuelQuantity > TankCapacity)
            {
                Console.WriteLine($"Cannot fit {fuel} fuel in the tank");
                return;
            }
            FuelQuantity += fuel;
        }

        public override string ToString()
        {
            return $"{this.GetType().Name}: {FuelQuantity:f2}";
        }
    }
}
