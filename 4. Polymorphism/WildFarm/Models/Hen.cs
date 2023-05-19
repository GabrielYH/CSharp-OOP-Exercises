using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WildFarm.Models.Interfaces;

namespace WildFarm.Models
{
    public class Hen : Bird
    {
        private double weightIncreasePerFood = 0.35;
        public Hen(string name, double weight, int foodEaten, double wingSize) : base(name, weight, foodEaten, wingSize)
        {
        }

        public override void ProduceSound()
        {
            Console.WriteLine($"Cluck");
        }

        public override void Eat(IFood food)
        {
            FoodEaten += food.Quantity;
            Weight += weightIncreasePerFood * food.Quantity;
        }
    }
}
