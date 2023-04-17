using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilitaryElite
{
    public class Spy : SpecialisedSoldier
    {
        public Spy(string id, string firstName, string lastName, decimal salary, string corps, string codeNumber) : base(id, firstName, lastName, salary, corps)
        {
            CodeNumber = codeNumber;
        }
        public string CodeNumber { get; set; }
        public override string ToString()
        {
            StringBuilder sb = new();
            sb.AppendLine($"Name: {FirstName} {LastName} Id: {Id} Salary: {Salary:f2}");
            sb.AppendLine($"Code Number: {CodeNumber}");
            return sb.ToString().Trim();
        }
    }
}
