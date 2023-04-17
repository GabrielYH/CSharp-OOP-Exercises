using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace MilitaryElite
{
    public class Commando : SpecialisedSoldier
    {

        public Commando(string id, string firstName, string lastName, decimal salary, string corps, params (string, string)[] missions) : base(id, firstName, lastName, salary, corps)
        {
            Missions = new();
            foreach (var mission in missions)
            {
                
                AddMission(mission.Item1, mission.Item2);

            }
        }
        public List<(string, string)> Missions { get; private set; }

        private void AddMission(string codeName, string state)
        {
            if (state != "inProgress" && state != "Finished")
            {

            }
            else
            {
                Missions.Add((codeName, state));
            }
        }
        public void CompleteMission()
        {

        }

        public override string ToString()
        {
            StringBuilder sb = new();
            sb.AppendLine($"Name: {FirstName} {LastName} Id: {Id} Salary: {Salary:f2}");
            sb.AppendLine($"Corps: {Corps}");
            sb.AppendLine($"Missions:");
            foreach (var (codeName, state) in this.Missions)
            {
                sb.AppendLine($" Code Name: {codeName} State: {state}");
            }
            return sb.ToString().Trim();
        }

    }
}
