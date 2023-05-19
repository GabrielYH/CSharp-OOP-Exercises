using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WildFarm.Factories.Interfaces;
using WildFarm.Models;
using WildFarm.Models.Interfaces;

namespace WildFarm.Factories
{
    public class AnimalFactory : IAnimalFactory
    {
        public IAnimal CreateAnimal(string[] cmdArgs)
        {
            string type = cmdArgs[0];
            string name = cmdArgs[1];
            double weight = double.Parse(cmdArgs[2]);
            int foodEaten = 0;
            Animal animal;
            if (type == "Owl")
            {
                animal = new Owl(name, weight, foodEaten, double.Parse(cmdArgs[3]));
            }
            else if (type == "Hen")
            {
                animal = new Hen(name, weight, foodEaten, double.Parse(cmdArgs[3]));
            }
            else if (type == "Mouse")
            {
                animal = new Mouse(name, weight, foodEaten, cmdArgs[3]);
            }
            else if (type == "Dog")
            {
                animal = new Dog(name, weight, foodEaten, cmdArgs[3]);
            }
            else if (type == "Cat")
            {
                animal = new Cat(name, weight, foodEaten, cmdArgs[3], cmdArgs[4]);
            }
            else if (type == "Tiger")
            {
                animal = new Tiger(name, weight, foodEaten, cmdArgs[3], cmdArgs[4]);
            }
            else
            {
                return default;
            }
            return animal;
        }
    }
}
