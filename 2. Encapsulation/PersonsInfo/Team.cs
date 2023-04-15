using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonsInfo
{
    public class Team
    {
        private string name;
        private List<Person> firstTeam;
        private List<Person> reserveTeam;
        public Team(string name)
        {
            this.name = name;
            this.firstTeam = new();
            this.reserveTeam = new();
        }

        public IReadOnlyCollection<Person> FirstTeam { get { return firstTeam; } }
        public IReadOnlyCollection<Person> ReserveTeam { get { return reserveTeam; } }

        public void AddPlayer(Person person)
        {
            if (person.Age < 40)
            {
                this.firstTeam.Add(person);
            }
            else
            {
                this.reserveTeam.Add(person);
            }
        }
        public override string ToString()
        {
            return $"First team has {firstTeam.Count} players." + Environment.NewLine + $"Reserve team has {reserveTeam.Count} players.";
        }

    }
}
