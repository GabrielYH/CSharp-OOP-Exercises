using System;
namespace PlayersAndMonsters
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            SoulMaster hero1 = new("BlondyWolfa", 104);
            DarkWizard hero2 = new("DriftWorks", 64);
            Console.WriteLine(hero1);
            Console.WriteLine(hero2);
        }
    }
}