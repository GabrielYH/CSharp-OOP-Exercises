using System;
namespace PersonInfo
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            string name = Console.ReadLine();
            int age = int.Parse(Console.ReadLine());
            string id = Console.ReadLine();
            string birthdate = Console.ReadLine();

            IIdentifiable identifiableCitizen = new Citizen(name, age, id, birthdate);
            IBirthable birthableCitizen = new Citizen(name, age, id, birthdate);
            Console.WriteLine(identifiableCitizen.Id);
            Console.WriteLine(birthableCitizen.Birthdate);
        }
    }
}