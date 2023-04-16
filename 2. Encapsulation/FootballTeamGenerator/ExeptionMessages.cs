using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballTeamGenerator
{
    public static class ExeptionMessages
    {
        public const string nameNotNullOrEmpty = "A name should not be empty.";
        public const string statsOutOfRange = "{0} should be between 0 and 100.";
        public const string missingPlayer = "Player {0} is not in {1} team."; 
        public const string missingTeam = "Team {0} does not exist.";
        public const string statsForMissingTeam = "Team {0} does not exist.";
    }
}
