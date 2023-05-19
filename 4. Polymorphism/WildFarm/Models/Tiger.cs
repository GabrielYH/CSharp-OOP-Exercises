using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WildFarm.Models.Interfaces;

namespace WildFarm.Models
{
    public class Tiger : Feline
    {
        private double weightIncreasePerFood = 1;
        public Tiger(string name, double weight, int foodEaten, string livingRegion, string breed) : base(name, weight, foodEaten, livingRegion, breed)
        {
        }
        public override void ProduceSound()
        {
            Console.WriteLine($"ROAR!!!");
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
