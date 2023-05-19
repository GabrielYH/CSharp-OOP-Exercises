using CarRacing.Models.Cars.Contracts;
using CarRacing.Models.Racers.Contracts;
using CarRacing.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRacing.Models.Racers
{
    public abstract class Racer : IRacer
    {
        private string username;
        private string racingBehavior;
        private int drivingExperience;
        private ICar car;
        public Racer(string username, string racingBehavior, int drivingExperience, ICar car)
        {
            Username = username;
            RacingBehavior = racingBehavior;
            DrivingExperience = drivingExperience;
            Car = car;
        }
        public string Username
        {
            get => this.username;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.InvalidRacerName));
                }
                this.username = value;
            }
        }

        public string RacingBehavior
        {
            get => this.racingBehavior;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.InvalidRacerBehavior));
                }
                this.racingBehavior = value;
            }
        }

        public int DrivingExperience
        {
            get => this.drivingExperience;
            private set
            {
                if (value < 0 || value > 100)
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.InvalidRacerDrivingExperience));
                }
                this.drivingExperience = value;
            }
        }

        public ICar Car 
        {
            get => this.car;
            private set
            {
                if (value == null) // check
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.InvalidRacerCar));
                }
                this.car = value;
            }
        }

        public bool IsAvailable()
        {
            return Car.FuelAvailable > Car.FuelConsumptionPerRace; // check
        }

        public void Race() // check
        {
            this.Car.Drive();
            if (this.GetType().Name == "ProfessionalRacer")
            {
                this.DrivingExperience += 10;
            }
            else if (this.GetType().Name == "StreetRacer")
            {
                this.DrivingExperience += 5;
            }
        }


    }
}
