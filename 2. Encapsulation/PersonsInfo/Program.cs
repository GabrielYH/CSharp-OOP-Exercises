using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;

namespace PersonsInfo
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            Team team = new("SoftUni");
            int n = int.Parse(Console.ReadLine());
            for (int i = 0; i < n; i++)
            {
                try
                {
                    string[] personInfo = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);
                    Person person = new(personInfo[0], personInfo[1], int.Parse(personInfo[2]), decimal.Parse(personInfo[3]));
                    team.AddPlayer(person);
                    
                }
                catch (ArgumentException ex)
                {

                    Console.WriteLine(ex.Message);
                }
            }

            Console.WriteLine(team);




        }
    }
}