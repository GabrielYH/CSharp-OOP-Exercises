using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FootballTeamGenerator
{

    public class Player
    {
        private string name;
        private double endurance;
        private double sprint;
        private double dribble;
        private double passing;
        private double shooting;

        public Player(string name, double endurance, double sprint, double dribble, double passing, double shooting)
        {
            Name = name;
            Endurance = endurance;
            Sprint = sprint;
            Dribble = dribble;
            Passing = passing;
            Shooting = shooting;
        }
        //only name and stats should be visible
        public string Name
        {
            get { return name; }
            set
            {
                if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(string.Format(ExeptionMessages.nameNotNullOrEmpty, nameof(this.Name)));
                }
                this.name = value;
            }
        }
        public double Endurance
        {
            get { return this.endurance; }
            set
            {
                if (value < 0 || value > 100)
                {
                    throw new ArgumentException(string.Format(ExeptionMessages.statsOutOfRange, nameof(this.Endurance)));
                }
                this.endurance = value;
            }
        }
        public double Sprint
        {
            get { return this.sprint; }
            set
            {
                if (value < 0 || value > 100)
                {
                    throw new ArgumentException(string.Format(ExeptionMessages.statsOutOfRange, nameof(this.Sprint)));
                }
                this.sprint = value;
            }
        }

        public double Dribble
        {
            get { return this.dribble; }
            set
            {
                if (value < 0 || value > 100)
                {
                    throw new ArgumentException(string.Format(ExeptionMessages.statsOutOfRange, nameof(this.Dribble)));
                }
                this.dribble = value;
            }
        }
        public double Passing
        {
            get { return this.passing; }
            set
            {
                if (value < 0 || value > 100)
                {
                    throw new ArgumentException(string.Format(ExeptionMessages.statsOutOfRange, nameof(this.Passing)));
                }
                this.passing = value;
            }
        }
        public double Shooting
        {
            get { return this.shooting; }
            set
            {
                if (value < 0 || value > 100)
                {
                    throw new ArgumentException(string.Format(ExeptionMessages.statsOutOfRange, nameof(this.Shooting)));
                }
                this.shooting = value;
            }
        }

        private double CalculateSkillLevel()
        {
            double skillLevel = (Shooting + Passing + Dribble + Endurance + Sprint) / 5;
            return skillLevel;
        }
    }
}
