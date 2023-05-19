using Formula1.Core.Contracts;
using Formula1.Models;
using Formula1.Models.Contracts;
using Formula1.Repositories;
using Formula1.Repositories.Contracts;
using Formula1.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formula1.Core
{
    public class Controller : IController
    {
        private PilotRepository pilotRepository;
        private RaceRepository raceRepository;
        private FormulaOneCarRepository carRepository;
        public Controller()
        {
            pilotRepository = new();
            raceRepository = new();
            carRepository = new();
        }
        public string AddCarToPilot(string pilotName, string carModel)
        {
            IPilot pilot = this.pilotRepository.Models.FirstOrDefault(p => p.FullName == pilotName);
            if (pilot == null || pilot.Car != null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.PilotDoesNotExistOrHasCarErrorMessage, pilotName));
            }
            IFormulaOneCar car = this.carRepository.Models.FirstOrDefault(c => c.Model == carModel);
            if (car == null)
            {
                throw new NullReferenceException(string.Format(ExceptionMessages.CarDoesNotExistErrorMessage, carModel));
            }
            pilot.AddCar(car);
            this.carRepository.Remove(car);
            return string.Format(OutputMessages.SuccessfullyPilotToCar, pilotName, car.GetType().Name, carModel);
        }

        public string AddPilotToRace(string raceName, string pilotFullName)
        {
            IRace race = this.raceRepository.Models.FirstOrDefault(r => r.RaceName == raceName);
            if (race == null)
            {
                throw new NullReferenceException(string.Format(ExceptionMessages.RaceDoesNotExistErrorMessage, raceName));
            }
            IPilot pilot = this.pilotRepository.Models.FirstOrDefault(p => p.FullName == pilotFullName);
            if (pilot == null || pilot.CanRace == false || race.Pilots.Any(p=> p.FullName == pilotFullName))
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.PilotDoesNotExistErrorMessage, pilotFullName));
            }
            race.AddPilot(pilot);
            
            return string.Format(OutputMessages.SuccessfullyAddPilotToRace, pilotFullName, raceName);
        }

        public string CreateCar(string type, string model, int horsepower, double engineDisplacement)
        {
            if (this.carRepository.FindByName(model) != null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.CarExistErrorMessage, model));
            }
            if (type == "Ferrari")
            {
                IFormulaOneCar ferrari = new Ferrari(model, horsepower, engineDisplacement);
                this.carRepository.Add(ferrari);
                return string.Format(OutputMessages.SuccessfullyCreateCar, type, model);
            }
            else if (type == "Williams")
            {
                IFormulaOneCar williams = new Williams(model, horsepower, engineDisplacement);
                this.carRepository.Add(williams);
                return string.Format(OutputMessages.SuccessfullyCreateCar, type, model);
            }
            else
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.InvalidTypeCar, type));
            }

        }

        public string CreatePilot(string fullName)
        {
            if (this.pilotRepository.FindByName(fullName) != null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.PilotExistErrorMessage, fullName));
            }
            else
            {
                IPilot pilotToBeAdded = new Pilot(fullName);
                this.pilotRepository.Add(pilotToBeAdded);
                return string.Format(OutputMessages.SuccessfullyCreatePilot, fullName);
            }

        }

        public string CreateRace(string raceName, int numberOfLaps)
        {
            if (this.raceRepository.FindByName(raceName) != null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.RaceExistErrorMessage, raceName));
            }
            IRace race = new Race(raceName, numberOfLaps);
            this.raceRepository.Add(race);
            return string.Format(OutputMessages.SuccessfullyCreateRace, raceName);
        }

        public string PilotReport()
        {
            StringBuilder sb = new();
            foreach (var pilot in this.pilotRepository.Models.OrderByDescending(p => p.NumberOfWins))
            {
                sb.AppendLine(pilot.ToString());
            }
            return sb.ToString().Trim();
        }

        public string RaceReport()
        {
            StringBuilder sb = new();
            foreach (var executedRace in this.raceRepository.Models.Where(r => r.TookPlace == true))
            {
                sb.AppendLine(executedRace.RaceInfo());
            }
            return sb.ToString().Trim();
        }

        public string StartRace(string raceName)
        {

            IRace race = this.raceRepository.FindByName(raceName);
            if (race == null)
            {
                throw new NullReferenceException(string.Format(ExceptionMessages.RaceDoesNotExistErrorMessage, raceName));
            }
            else if (race.Pilots.Count < 3)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.InvalidRaceParticipants, raceName));
            }
            else if (race.TookPlace)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.RaceTookPlaceErrorMessage, raceName));
            }
            List<IPilot> pilots = race.Pilots.OrderByDescending(p => p.Car.RaceScoreCalculator(race.NumberOfLaps)).ToList();
            race.TookPlace = true;
            pilots[0].WinRace();
            StringBuilder sb = new();
            sb.AppendLine(string.Format(OutputMessages.PilotFirstPlace, pilots[0].FullName, raceName));
            sb.AppendLine(string.Format(OutputMessages.PilotSecondPlace, pilots[1].FullName, raceName));
            sb.AppendLine(string.Format(OutputMessages.PilotThirdPlace, pilots[2].FullName, raceName));
            return sb.ToString().Trim();
        }
    }
}
