using PlanetWars.Models.MilitaryUnits.Contracts;
using PlanetWars.Models.Planets.Contracts;
using PlanetWars.Models.Weapons.Contracts;
using PlanetWars.Repositories.Contracts;
using PlanetWars.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using PlanetWars.Repositories;

namespace PlanetWars.Models.Planets
{
    public class Planet : IPlanet
    {
        private string name;
        private double budget;
        private double militaryPower;
        private IRepository<IMilitaryUnit> army; // dali ne trq smenq imeto
        private IRepository<IWeapon> weapons; // dali ne trq smenq imeto
        public Planet(string name, double budget)
        {
            Name = name;
            Budget = budget;
            this.army = new UnitRepository();
            this.weapons = new WeaponRepository();
        }
        public string Name
        {
            get => this.name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.InvalidPlanetName));
                }
                this.name = value;
            }
        }

        public double Budget
        {
            get => this.budget;
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.InvalidBudgetAmount));
                }
                this.budget = value;
            }
        }

        public double MilitaryPower { get { return CalculateMilitaryPower(); } }



        public IReadOnlyCollection<IMilitaryUnit> Army { get { return this.army.Models; } }


        public IReadOnlyCollection<IWeapon> Weapons { get { return this.weapons.Models; } }


        public void AddUnit(IMilitaryUnit unit)
        {
            this.army.AddItem(unit);
        }

        public void AddWeapon(IWeapon weapon)
        {
            this.weapons.AddItem(weapon);
        }

        public string PlanetInfo()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Planet: {Name}");
            sb.AppendLine($"--Budget: {Budget} billion QUID");
            if (this.army.Models.Count == 0)
            {
                sb.AppendLine($"No units");
            }
            else
            {
                sb.AppendLine($"--Forces: {string.Join(", ", this.army.Models.Select(u => u.GetType().Name))}"); //chiek
            }
            if (this.weapons.Models.Count == 0)
            {
                sb.AppendLine($"No weapons");
            }
            else
            {
                sb.AppendLine($"--Combat equipment: {string.Join(", ", this.weapons.Models.Select(u => u.GetType().Name))}"); // chiek
            }
            sb.AppendLine($"--Military Power: {MilitaryPower}");
            return sb.ToString().Trim();
        }

        public void Profit(double amount)
        {
            Budget += amount;
        }

        public void Spend(double amount)
        {
            if (Budget < amount)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.UnsufficientBudget));
            }
            Budget -= amount;
        }

        public void TrainArmy()
        {
            foreach (var unit in this.army.Models)
            {
                unit.IncreaseEndurance(); // bi trqlo da e taka
            }
        }

        private double CalculateMilitaryPower()
        {
            double sumOfEndurances = 0;
            double sumOfDestructions = 0;
            foreach (var unit in this.army.Models)
            {
                sumOfEndurances += unit.EnduranceLevel;
            }
            foreach (var weapon in this.weapons.Models)
            {
                sumOfDestructions += weapon.DestructionLevel;
            }
            double totalAmount = sumOfEndurances + sumOfDestructions;
            if (this.army.Models.Any(u => u.GetType().Name == "AnonymousImpactUnit")) // dali e taka
            {
                totalAmount += totalAmount * 0.3; // dali e taka
            }
            if (this.weapons.Models.Any(w => w.GetType().Name == "NuclearWeapon"))
            {
                totalAmount += totalAmount * 0.45; // dali e taka
            }
            return Math.Round(totalAmount, 3); // check
        }
    }
}
