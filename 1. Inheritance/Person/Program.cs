using System;

namespace Person
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            var name = Console.ReadLine();
            var age = int.Parse(Console.ReadLine());

            if (age > 15)
            {
                Person person = new(name, age);
                Console.WriteLine(person);
            }
            else if (age <= 15)
            {
                Child child = new(name, age);
                Console.WriteLine(child);
            }
           
        }
    }
}