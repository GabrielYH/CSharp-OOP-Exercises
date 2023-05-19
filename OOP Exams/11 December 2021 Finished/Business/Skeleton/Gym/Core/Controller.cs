using Gym.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Gym.Repositories.Contracts;
using Gym.Models.Equipment.Contracts;
using Gym.Models.Gyms.Contracts;
using Gym.Repositories;
using Gym.Utilities.Messages;
using Gym.Models.Gyms;
using Gym.Models.Equipment;
using Gym.Models.Athletes.Contracts;
using Gym.Models.Athletes;

namespace Gym.Core
{
    public class Controller : IController
    {
        private IRepository<IEquipment> EquipmentRepository;
        private List<IGym> gyms;
        public Controller()
        {
            this.EquipmentRepository = new EquipmentRepository();
            gyms = new List<IGym>();
        }
        public string AddAthlete(string gymName, string athleteType, string athleteName, string motivation, int numberOfMedals)
        {
            IGym gym = this.gyms.FirstOrDefault(g => g.Name == gymName);
            if (athleteType != "Boxer" && athleteType != "Weightlifter")
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.InvalidAthleteType));
            }

            IAthlete athlete;
            if (athleteType == "Boxer")
            {
                athlete = new Boxer(athleteName, motivation, numberOfMedals);
            }
            else if (athleteType == "Weightlifter")
            {
                athlete = new Weightlifter(athleteName, motivation, numberOfMedals);
            }
            else
            {
                athlete = null;//
            }

            if (gym.GetType().Name == "BoxingGym")
            {
                if (athleteType == "Weightlifter")
                {
                    return string.Format(OutputMessages.InappropriateGym);
                }
                else if (athleteType == "Boxer")
                {
                    gym.AddAthlete(athlete);
                    return string.Format(OutputMessages.EntityAddedToGym,athleteType, gymName);
                }
                else
                {
                    return null;//
                }
            }
            else if (gym.GetType().Name == "WeightliftingGym")
            {
                if (athleteType == "Boxer")
                {
                    return string.Format(OutputMessages.InappropriateGym);
                }
                else if (athleteType == "Weightlifter")
                {
                    gym.AddAthlete(athlete);
                    return string.Format(OutputMessages.EntityAddedToGym, athleteType, gymName);
                }
                else
                {
                    return null;//
                }
            }
            else
            {
                return null;//
            }
            
        }

        public string AddEquipment(string equipmentType)
        {
            if (equipmentType != "BoxingGloves" && equipmentType != "Kettlebell")
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.InvalidEquipmentType));
            }

            IEquipment equipment;
            if (equipmentType == "BoxingGloves")
            {
                equipment = new BoxingGloves();
            }
            else if (equipmentType == "Kettlebell")
            {
                equipment = new Kettlebell();
            }
            else
            {
                equipment = null;//
            }
            this.EquipmentRepository.Add(equipment);
            return string.Format(OutputMessages.SuccessfullyAdded, equipmentType);
        }

        public string AddGym(string gymType, string gymName)
        {
            if (gymType != "BoxingGym" && gymType != "WeightliftingGym")
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.InvalidGymType));
            }
            IGym gym;
            if (gymType == "BoxingGym")
            {
                gym = new BoxingGym(gymName);
            }
            else if (gymType == "WeightliftingGym")
            {
                gym = new WeightliftingGym(gymName);
            }
            else
            {
                gym = null;//
            }
            this.gyms.Add(gym);
            return string.Format(OutputMessages.SuccessfullyAdded, gymType);
        }

        public string EquipmentWeight(string gymName)
        {
            IGym gym = this.gyms.FirstOrDefault(g => g.Name == gymName);
            return string.Format(OutputMessages.EquipmentTotalWeight, gymName , gym.EquipmentWeight); // checkkkkk
        }

        public string InsertEquipment(string gymName, string equipmentType)
        {
            IEquipment equipment = this.EquipmentRepository.FindByType(equipmentType);
            if (equipment == null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.InexistentEquipment, equipmentType));
            }
            IGym gym = this.gyms.FirstOrDefault(g => g.Name == gymName);
            gym.AddEquipment(equipment);
            this.EquipmentRepository.Remove(equipment);
            return string.Format(OutputMessages.EntityAddedToGym, equipmentType, gymName);
        }

        public string Report()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var gym in this.gyms)
            {
                sb.AppendLine($"{gym.GymInfo()}");
            }
            return sb.ToString().Trim();
        }

        public string TrainAthletes(string gymName)
        {
            IGym gym = this.gyms.FirstOrDefault(g => g.Name == gymName);
            gym.Exercise(); //check
            return string.Format(OutputMessages.AthleteExercise, gym.Athletes.Count); //check ?
        }
    }
}
