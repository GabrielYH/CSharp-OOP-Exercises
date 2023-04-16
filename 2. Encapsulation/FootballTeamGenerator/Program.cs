using System;
using System.Linq;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Reflection.Metadata.Ecma335;

namespace FootballTeamGenerator
{
    public class StartUp
    {
        static void Main(string[] args)
        {

            List<Team> teams = new();
            string command;
            while ((command = Console.ReadLine()) != "END")
            {
                try
                {
                    string[] cmdArgs = command.Split(";", StringSplitOptions.RemoveEmptyEntries);
                    string teamName = cmdArgs[1];
                    if (cmdArgs[0] == "Team")
                    {
                        Team team = new(teamName);
                        teams.Add(team);
                    }
                    else if (cmdArgs[0] == "Add")
                    {
                        if (teams.Any(x => x.Name == teamName))
                        {
                            string playerName = cmdArgs[2];
                            int endurance = int.Parse(cmdArgs[3]);
                            int sprint = int.Parse(cmdArgs[4]);
                            int dribble = int.Parse(cmdArgs[5]);
                            int passing = int.Parse(cmdArgs[6]);
                            int shooting = int.Parse(cmdArgs[7]);
                            Player player = new(playerName, endurance, sprint, dribble, passing, shooting);
                            Team team = teams.FirstOrDefault(x => x.Name == teamName);
                            team.AddPlayer(player);
                        }
                        else
                        {
                            throw new ArgumentException(string.Format(ExeptionMessages.missingTeam, teamName));
                        }
                    }
                    else if (cmdArgs[0] == "Remove")
                    {
                        if (teams.Any(x => x.Name == teamName))
                        {
                            string playerName = cmdArgs[2];
                            Team team = teams.FirstOrDefault(x => x.Name == teamName);
                            team.RemovePlayer(playerName);
                        }
                        else
                        {
                            throw new ArgumentException(string.Format(ExeptionMessages.missingTeam, teamName));
                        }

                    }
                    else if (cmdArgs[0] == "Rating")
                    {
                        if (teams.Any(x => x.Name == teamName))
                        {
                            Team team = teams.FirstOrDefault(x => x.Name == teamName);
                            Console.WriteLine($"{team.Name} - {team.Rating}");
                        }
                        else
                        {
                            throw new ArgumentException(string.Format(ExeptionMessages.missingTeam, teamName));
                        }
                    }
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);

                }
            }
        }
    }
}