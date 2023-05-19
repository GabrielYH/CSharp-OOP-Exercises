using CarRacing.Models.Cars.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRacing.Models.Racers
{
    public class StreetRacer : Racer
    {
        private const int drivingExp = 10;
        private const string behavior = "aggressive";
        public StreetRacer(string username, ICar car) : base(username, behavior, drivingExp, car)
        {
        }
    }
}
