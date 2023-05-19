using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WildFarm.Models.Interfaces;

namespace WildFarm.Models
{
    public class Dog : Mammal
    {
        private double weightIncreasePerFood = 0.40;
        public Dog(string name, double weight, int foodEaten, string livingRegion) : base(name, weight, foodEaten, livingRegion)
        {
        }

        public override void ProduceSound()
        {
            Console.WriteLine($"Woof!");
        }

        public override void Eat(IFood food)
        {
            if (food.GetType().Name != "Meat")
            {
                throw new ArgumentException($"{this.GetType().Name} does not eat {food.GetType().Name}!");
            }
            FoodEaten += food.Quantity;
            Weight += weightIncreasePerFood * food.Quantity;
        }
    }
}
