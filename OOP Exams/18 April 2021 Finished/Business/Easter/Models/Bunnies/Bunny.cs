using Easter.Models.Bunnies.Contracts;
using Easter.Models.Dyes.Contracts;
using Easter.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace Easter.Models.Bunnies
{
    public abstract class Bunny : IBunny
    {
        private string name;
        private int energy;
        private List<IDye> dyes; // ?
        public Bunny(string name, int energy)
        {
            Name = name;
            Energy = energy;
            this.dyes = new List<IDye>();
        }
        public string Name
        {
            get => this.name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.InvalidBunnyName));
                }
                this.name = value;
            }
        }

        public int Energy
        {
            get => this.energy;
            protected set
            {
                if (value < 0)
                {
                    this.energy = 0;
                }
                this.energy = value;
            }
        }

        public ICollection<IDye> Dyes => this.dyes; // dali trq ima field setter nesh

        public void AddDye(IDye dye)
        {
            Dyes.Add(dye);
        }

        public abstract void Work();
        
    }
}
