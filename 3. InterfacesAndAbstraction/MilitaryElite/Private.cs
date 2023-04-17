using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace MilitaryElite
{
    public class Private : Soldier
    {
        public Private(string id, string firstName, string lastName, decimal salary) : base(id,firstName,lastName)
        {
            Salary = salary;
        }
        public decimal Salary { get; set; }

        public override string ToString()
        {
            return $"Name: {FirstName} {LastName} Id: {Id} Salary: {Salary}";
        }
    }
}
