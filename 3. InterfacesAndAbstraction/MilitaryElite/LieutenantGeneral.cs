using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilitaryElite
{
    public class LieutenantGeneral: Private
    {
        public LieutenantGeneral(string id, string firstName, string lastName, decimal salary, params string[] privatesIds): base(id,firstName, lastName, salary)
        {
            PrivatesIds = privatesIds.ToList(); //
        }
        public List<string> PrivatesIds { get; private set; }
        public List<Private> Privates { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new();
            sb.AppendLine($"Name: {FirstName} {LastName} Id: {Id} Salary: {Salary:f2}");
            sb.AppendLine($"Privates:");
            foreach (var id in PrivatesIds)
            {
                if (Privates.Any(x => x.Id == id))
                {
                    Private currentPrivate = Privates.FirstOrDefault(x => x.Id == id);
                    sb.AppendLine(currentPrivate.ToString());
                }
            }
            return sb.ToString().Trim();
        }
    }
}
