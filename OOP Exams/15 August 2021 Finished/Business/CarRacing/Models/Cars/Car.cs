using CarRacing.Models.Cars.Contracts;
using CarRacing.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRacing.Models.Cars
{
    public abstract class Car : ICar
    {
        private string make;
        private string model;
        private string vin;
        private int horsePower;
        private double fuelAvailable;
        private double fuelConsumptionPerRace;
        public Car(string make, string model, string VIN, int horsePower, double fuelAvailable, double fuelConsumptionPerRace)
        {
            Make = make;
            Model = model;
            this.VIN = VIN;
            HorsePower = horsePower;
            FuelAvailable = fuelAvailable;
            FuelConsumptionPerRace = fuelConsumptionPerRace;

        }
        public string Make
        {
            get => this.make;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.InvalidCarMake));
                }
                this.make = value;
            }
        }

        public string Model
        {
            get => this.model;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.InvalidCarModel));
                }
                this.model = value;
            }
        }

        public string VIN
        {
            get => this.vin;
            private set
            {
                if (value.Length != 17)
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.InvalidCarVIN));
                }
                this.vin = value;
            }
        }

        public int HorsePower
        {
            get => this.horsePower;
            protected set
            {
                if (value < 0)
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.InvalidCarHorsePower));
                }
                this.horsePower = value;
            }
        }


        public double FuelAvailable
        {
            get => this.fuelAvailable;
            private set
            {
                if (value < 0)
                {
                    this.fuelAvailable = 0;
                }
                this.fuelAvailable = value;
            }

        }

        public double FuelConsumptionPerRace
        {
            get => this.fuelConsumptionPerRace;
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.InvalidCarFuelConsumption));
                }
                this.fuelConsumptionPerRace = value;
            }
        }

        public void Drive()
        {
            if (this.GetType().Name == "TunedCar")
            {
                HorsePower -= (int)Math.Round(HorsePower * 0.03); // check
            }
            if (FuelAvailable -FuelConsumptionPerRace < 0)
            {
                FuelAvailable = 0;
            }
            else
            {
                FuelAvailable -= FuelConsumptionPerRace;
            }
                //      
        }
    }
}
