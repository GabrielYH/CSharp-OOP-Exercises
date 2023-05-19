using CarRacing.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using CarRacing.Repositories.Contracts;
using CarRacing.Models.Cars.Contracts;
using CarRacing.Models.Racers.Contracts;
using CarRacing.Models.Maps.Contracts;
using CarRacing.Repositories;
using CarRacing.Models.Maps;
using CarRacing.Utilities.Messages;
using CarRacing.Models.Cars;
using CarRacing.Models.Racers;

namespace CarRacing.Core
{
    public class Controller : IController
    {
        IRepository<ICar> CarRepository;
        IRepository<IRacer> RacerRepository;
        IMap map;
        public Controller()
        {
            this.CarRepository = new CarRepository();
            this.RacerRepository = new RacerRepository();
            this.map = new Map();
        }
        public string AddCar(string type, string make, string model, string VIN, int horsePower)
        {
            if (type != "SuperCar" && type != "TunedCar")
            {
                throw new ArgumentException(string.Format(ExceptionMessages.InvalidCarType));
            }
            ICar car;
            if (type == "SuperCar")
            {
                car = new SuperCar(make, model, VIN, horsePower);
            }
            else if (type == "TunedCar")
            {
                car = new TunedCar(make, model, VIN, horsePower);
            }
            else
            {
                car = null;
            }
            this.CarRepository.Add(car);
            return string.Format(OutputMessages.SuccessfullyAddedCar, make, model, VIN);
        }

        public string AddRacer(string type, string username, string carVIN)
        {
            ICar car = this.CarRepository.FindBy(carVIN);
            if (car == null)
            {
                throw new ArgumentException(string.Format(ExceptionMessages.CarCannotBeFound));
            }
            if (type != "StreetRacer" && type != "ProfessionalRacer")
            {
                throw new ArgumentException(string.Format(ExceptionMessages.InvalidRacerType));
            }
            IRacer racer;
            if (type == "StreetRacer")
            {
                racer = new StreetRacer(username, car);
            }
            else if (type == "ProfessionalRacer")
            {
                racer = new ProfessionalRacer(username, car);
            }
            else
            {
                racer = null;
            }
            this.RacerRepository.Add(racer);
            return string.Format(OutputMessages.SuccessfullyAddedRacer, username);
        }

        public string BeginRace(string racerOneUsername, string racerTwoUsername)
        {
            IRacer racerOne = this.RacerRepository.FindBy(racerOneUsername);
            if (racerOne == null)
            {
                throw new ArgumentException(string.Format(ExceptionMessages.RacerCannotBeFound, racerOneUsername));
            }
            IRacer racerTwo = this.RacerRepository.FindBy(racerTwoUsername);
            if (racerTwo == null)
            {
                throw new ArgumentException(string.Format(ExceptionMessages.RacerCannotBeFound, racerTwoUsername));
            }
            return map.StartRace(racerOne, racerTwo);
        }

        public string Report()
        {
            StringBuilder sb = new StringBuilder();
            List<IRacer> ordered = this.RacerRepository.Models.OrderByDescending(r => r.DrivingExperience).ThenBy(r => r.Username).ToList();
            foreach (var racer in ordered)
            {
                sb.AppendLine($"{racer.GetType().Name}: {racer.Username}");
                sb.AppendLine($"--Driving behavior: {racer.RacingBehavior}");
                sb.AppendLine($"--Driving experience: {racer.DrivingExperience}");
                sb.AppendLine($"--Car: {racer.Car.Make} {racer.Car.Model} ({racer.Car.VIN})");
            }
            return sb.ToString().Trim();
        }
    }
}
