using Gym.Models.Athletes.Contracts;
using Gym.Models.Equipment.Contracts;
using Gym.Models.Gyms.Contracts;
using Gym.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Gym.Models.Gyms
{
    public abstract class Gym : IGym
    {
        private string name;
        private int capacity;
        private double equipmentWeight;
        private List<IEquipment> equipment;
        private List<IAthlete> athletes;
        public Gym(string name, int capacity)
        {
            Name = name;
            Capacity = capacity;
            equipment = new List<IEquipment>();
            athletes = new List<IAthlete>();
        }
        public string Name
        {
            get => this.name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.InvalidGymName));
                }
                this.name = value;
            }
        }

        public int Capacity
        {
            get => this.capacity;
            private set => this.capacity = value;
        }

        public double EquipmentWeight
        {
            get
            {
                double sum = this.Equipment.Sum(e => e.Weight); // check
                return sum;
            }
            

        }

        public ICollection<IEquipment> Equipment => this.equipment;


        public ICollection<IAthlete> Athletes => this.athletes;
        

        public void AddAthlete(IAthlete athlete)
        {
            if (Athletes.Count < this.Capacity)
            {
                this.Athletes.Add(athlete);
            }
            else if (Athletes.Count >= Capacity)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.NotEnoughSize));
            }
        }

        public void AddEquipment(IEquipment equipment)
        {
            this.Equipment.Add(equipment);
        }

        public void Exercise()
        {
            foreach (var ath in Athletes)
            {
                ath.Exercise();
            }
        }

        public string GymInfo()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"{Name} is a {this.GetType().Name}:");
            if (this.Athletes.Count == 0)
            {
                sb.AppendLine($"Athletes: No athletes");
            }
            else
            {
                sb.AppendLine($"Athletes: {string.Join(", ", this.Athletes.Select(a => a.FullName))}"); // check no trq e vqrno
            }
            sb.AppendLine($"Equipment total count: {Equipment.Count}");
            sb.AppendLine($"Equipment total weight: {EquipmentWeight:f2} grams");
            return sb.ToString().Trim();
        }

        public bool RemoveAthlete(IAthlete athlete)
        {
            return this.Athletes.Remove(athlete);
        }
    }
}
