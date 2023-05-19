using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using WildFarm.Core.Interfaces;
using WildFarm.Factories;
using WildFarm.Factories.Interfaces;
using WildFarm.Models;
using WildFarm.Models.Interfaces;

namespace WildFarm.Core
{
    public class Engine : IEngine
    {
        public void Run()
        {
            List<IAnimal> animals = new();
            IFoodFactory foodFactory = new FoodFactory();
            IAnimalFactory animalFactory = new AnimalFactory();
            string command;
            int counter = 0;
            while ((command = Console.ReadLine()) != "End")
            {
                try
                {
                    string[] animalData = command
.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                    IAnimal animal = animalFactory.CreateAnimal(animalData);
                    animals.Add(animal);
                    string[] foodData = Console.ReadLine()
                    .Split(" ", StringSplitOptions.RemoveEmptyEntries);
                    IFood food = foodFactory.CreateFood(foodData[0], int.Parse(foodData[1]));
                    animal.ProduceSound();
                    animal.Eat(food);
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                counter++;
            }
            foreach (var animal in animals)
            {
                Console.WriteLine(animal.ToString());
            }
        }
    }
}
