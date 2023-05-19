using System;
using System.Collections.Generic;
using System.Text;

namespace CarRacing.Models.Cars
{
    public class TunedCar : Car
    {
        private const double fuelAvailable = 65;
        private const double fuelConsumption = 7.5;
        public TunedCar(string make, string model, string VIN, int horsePower) : base(make, model, VIN, horsePower, fuelAvailable, fuelConsumption)
        {
        }

        //public override void Drive()
        //{
        //    HorsePower -= (int)Math.Round(HorsePower * 0.03); // check
        //    if (FuelAvailable- FuelConsumptionPerRace < 0)
        //    {
        //        FuelAvailable = 0;
        //    }
        //    else
        //    {
        //        FuelAvailable -= FuelConsumptionPerRace;
        //    }
        //}
    }
}
