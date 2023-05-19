using CarRacing.Models.Maps.Contracts;
using CarRacing.Models.Racers.Contracts;
using CarRacing.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRacing.Models.Maps
{
    public class Map : IMap
    {
        public Map()
        {

        }
        public string StartRace(IRacer racerOne, IRacer racerTwo)
        {
            double strict = 1.2;
            double aggressive = 1.1;
            
            if (racerOne.IsAvailable() == false && racerTwo.IsAvailable() == false)
            {
                return string.Format(OutputMessages.RaceCannotBeCompleted);
            }
            if (racerOne.IsAvailable() == false) // 2nd win
            {
                return string.Format(OutputMessages.OneRacerIsNotAvailable, racerTwo.Username, racerOne.Username);
            }
            else if (racerTwo.IsAvailable() == false) // 1st win
            {
                return string.Format(OutputMessages.OneRacerIsNotAvailable, racerOne.Username, racerTwo.Username);
            }
            racerOne.Race();
            racerTwo.Race();

            double racerOneChance = 0;
            if (racerOne.RacingBehavior == "strict")
            {
                racerOneChance = racerOne.Car.HorsePower * racerOne.DrivingExperience * strict;
            }
            else if (racerOne.RacingBehavior == "aggressive")
            {
                racerOneChance = racerOne.Car.HorsePower * racerOne.DrivingExperience * aggressive;
            }

            double racerTwoChance = 0;
            if (racerTwo.RacingBehavior == "strict")
            {
                racerTwoChance = racerTwo.Car.HorsePower * racerTwo.DrivingExperience * strict;
            }
            else if (racerTwo.RacingBehavior == "aggressive")
            {
                racerTwoChance = racerTwo.Car.HorsePower * racerTwo.DrivingExperience * aggressive;
            }

            string winnerName = string.Empty;
            if (racerOneChance > racerTwoChance)
            {
                winnerName = racerOne.Username;
            }
            else if (racerOneChance < racerTwoChance)
            {
                winnerName = racerTwo.Username;
            }
            return string.Format(OutputMessages.RacerWinsRace, racerOne.Username, racerTwo.Username, winnerName);
        }
    }
}
