using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WildFarm.Models.Interfaces;

namespace WildFarm.Models
{
    public abstract class Animal : IAnimal
    {
        private double weightIncreasePerFood = 0;
        public Animal(string name, double weight, int foodEaten)
        {
            Name = name;
            Weight = weight;
            FoodEaten = foodEaten;
        }
        public string Name { get; }

        public double Weight { get; protected set; }

        public int FoodEaten { get; protected set; }

        public virtual void Eat(IFood food)
        {
            FoodEaten += food.Quantity;
            Weight += weightIncreasePerFood * food.Quantity;
        }

        public virtual void ProduceSound()
        {
            Console.WriteLine("Make Sound");
        }


    }
}
