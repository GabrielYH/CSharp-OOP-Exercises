using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilitaryElite
{
    public class Engineer : SpecialisedSoldier
    {
        public Engineer(string id, string firstName, string lastName, decimal salary, string corps, params(string,int)[] repairs) : base(id, firstName, lastName, salary, corps)
        {
            Repairs = repairs.ToList();
        }

        public List<(string,int)> Repairs { get; set; }
        public override string ToString()
        {
            StringBuilder sb = new();
            sb.AppendLine($"Name: {FirstName} {LastName} Id: {Id} Salary: {Salary:f2}");
            sb.AppendLine($"Corps: {Corps}");
            sb.AppendLine($"Repairs:");
            foreach (var (repair, hours) in this.Repairs)
            {
                sb.AppendLine($" Part Name: {repair} Hours Worked: {hours}");
            }
            return sb.ToString().Trim();
        }
    }
}
