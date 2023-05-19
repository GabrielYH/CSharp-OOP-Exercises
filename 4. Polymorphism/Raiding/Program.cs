using Raiding.Factories;
using Raiding.Factories.Interfaces;
using Raiding.Models;
using Raiding.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Raiding
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            IHeroFactory factory = new HeroFactory();
            List<IBaseHero> raidGroup = new();
            int n = int.Parse(Console.ReadLine());

            while (raidGroup.Count != n)
            {
                string heroName = Console.ReadLine();
                string heroType = Console.ReadLine();
                try
                {
                    IBaseHero createdHero = factory.CreateHero(heroType, heroName);
                    raidGroup.Add(createdHero);
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }

            int bossPower = int.Parse(Console.ReadLine());
            int powerSum = 0;
            foreach (var hero in raidGroup)
            {
                Console.WriteLine(hero.CastAbility());
                powerSum += hero.Power;
            }
            if (powerSum >= bossPower)
            {
                Console.WriteLine($"Victory!");
            }
            else
            {
                Console.WriteLine("Defeat...");
            }

        }
    }
}