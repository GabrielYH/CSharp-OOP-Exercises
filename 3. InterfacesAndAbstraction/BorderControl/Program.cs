using System;
using System.Collections.Generic;
using System.Linq;

namespace BorderControl
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            List<object> beings = new();
            int n = int.Parse(Console.ReadLine());
            for (int i = 0; i < n; i++)
            {
                string[] cmdArgs = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);
                if (cmdArgs.Length == 4)
                {
                    IBuyer citizen = new Citizen(cmdArgs[0], int.Parse(cmdArgs[1]), cmdArgs[2], cmdArgs[3]);
                    beings.Add(citizen);
                }
                else if (cmdArgs.Length == 3)
                {
                    IBuyer rebel = new Rebel(cmdArgs[0], int.Parse(cmdArgs[1]), cmdArgs[2]);
                    beings.Add(rebel);
                }
            }
            string command;
            int totalAmountFood = 0;
            while ((command = Console.ReadLine()) != "End")
            {
                string name = command;
                foreach (var obj in beings)
                {
                    if (obj is Citizen && ((Citizen)obj).Name == name)
                    {
                        ((Citizen)obj).BuyFood();
                        totalAmountFood += 10;
                    }
                    else if (obj is Rebel && ((Rebel)obj).Name == name)
                    {
                        ((Rebel)obj).BuyFood();
                        totalAmountFood += 5;
                    }
                }
            }
            Console.WriteLine(totalAmountFood);
        }
    }
}