using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballTeamGenerator
{
    public class Team
    {
        //only name, rating and methods for add and remove players
        private List<Player> players;
        private string name;
        public Team(string name)
        {
            players = new();
            Name = name;
        }

        public string Name
        {
            get { return name; }
            set
            {
                if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(string.Format(ExeptionMessages.nameNotNullOrEmpty, nameof(this.Name)));
                }
                this.name = value;
            }
        }
        public double Rating { get { return CalculateRating(); } }

        public void AddPlayer(Player player)
        {
            if (!players.Any(x => x.Name == player.Name))
            {
                players.Add(player);
            }
        }
        public void RemovePlayer(string playerName)
        {
            if (!players.Any(x => x.Name == playerName))
            {
                throw new ArgumentException(string.Format(ExeptionMessages.missingPlayer, playerName, Name));
            }
            Player playerToRemove = players.FirstOrDefault(x => x.Name == playerName);
            players.Remove(playerToRemove);
        }

        private double CalculateRating()
        {

            List<double> averageRatingOfEveryPlayer = new();
            foreach (var player in players)
            {
                double rating = 0;
                rating += (player.Endurance + player.Dribble + player.Shooting + player.Sprint + player.Passing) / 5;
                averageRatingOfEveryPlayer.Add(rating);
            }
            double ratingOfTeam = 0;
            if (averageRatingOfEveryPlayer.Any())
            {
               ratingOfTeam = averageRatingOfEveryPlayer.Average();
            }
            

            return Math.Round(ratingOfTeam);
        }
    }
}
