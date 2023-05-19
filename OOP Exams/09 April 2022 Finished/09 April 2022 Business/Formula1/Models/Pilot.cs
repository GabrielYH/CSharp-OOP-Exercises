﻿using Formula1.Models.Contracts;
using Formula1.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Formula1.Models
{
    public class Pilot : IPilot
    {
        private string fullName;
        private bool canRace = false;
        private IFormulaOneCar car;
        private int numberOfWins = 0;

        public Pilot(string fullName)
        {
            FullName = fullName;
        }
        public string FullName
        {
            get => this.fullName;
            private set
            {
                if (string.IsNullOrWhiteSpace(value) || value.Length < 5)
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.InvalidPilot, value));
                }
                this.fullName = value;
            }
        }

        public IFormulaOneCar Car
        {
            get => this.car;
            private set
            {
                if (value== null)
                {
                    throw new NullReferenceException(string.Format(ExceptionMessages.InvalidCarForPilot));
                }
                this.car = value;
            }
        }

        public int NumberOfWins { get { return this.numberOfWins; } } // tuka

        public bool CanRace { get { return this.canRace; } }

        public void AddCar(IFormulaOneCar car)
        {
            Car = car;
            canRace = true;
        }

        public void WinRace()
        {
            numberOfWins++;
        }

        public override string ToString()
        {
            return $"Pilot {FullName} has {numberOfWins} wins.";
        }
    }
}
