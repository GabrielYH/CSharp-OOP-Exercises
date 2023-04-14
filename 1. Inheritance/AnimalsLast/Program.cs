using System;
using System.Linq.Expressions;
using System.Text;

namespace Animals
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            StringBuilder sb = new();
            string command;

            while ((command = Console.ReadLine()) != "Beast!")
            {
                string type = command;
                string[] animalData = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);
                string name = animalData[0];
                int age = int.Parse(animalData[1]);
                string gender = animalData[2];
               
                if (type == "Cat")
                {
                    Cat cat = new(name, age, gender);
                    sb.AppendLine(cat.ToString());
                }
                else if (type == "Dog")
                {
                    Dog dog = new(name, age, gender);
                    sb.AppendLine(dog.ToString());
                }
                else if (type == "Frog")
                {
                    Frog frog = new(name, age, gender);
                    sb.AppendLine(frog.ToString());
                }
                else if (type == "Kitten")
                {
                    Kitten kitten = new(name, age);
                    sb.AppendLine(kitten.ToString());
                }
                else if (type == "Tomcat")
                {
                    Tomcat tomcat = new(name, age);
                    sb.AppendLine(tomcat.ToString());
                }
                else
                {
                    throw new ArgumentException("Invalid input!");
                }
                
            }
            Console.WriteLine(sb.ToString().Trim());
        }
    }
}