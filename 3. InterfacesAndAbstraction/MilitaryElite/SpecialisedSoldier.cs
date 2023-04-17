using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace MilitaryElite
{
    public class SpecialisedSoldier : Private
    {
        private string corps;
        public SpecialisedSoldier(string id, string firstName, string lastName, decimal salary, string corps) : base(id, firstName, lastName, salary)
        {
            Corps = corps;
        }
        public string Corps
        {
            get => this.corps;
            private set
            {
                if (value == "Airforces" || value == "Marines")
                {
                    this.corps = value;
                }
                else
                {
                    throw new ArgumentException("Invalid corps!");
                }
            }
        }
    }
}
