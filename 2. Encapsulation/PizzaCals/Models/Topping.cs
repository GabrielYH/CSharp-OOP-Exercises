using PizzaCals.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace PizzaCals.Models
{
    public class Topping
    {
        private const double BaseCaloriesPerGram = 2;
        private Toppings topping;
        private double toppingCalories;
        private double weight;
        public Topping(string toppingInput, double weight)
        {
            if (!Enum.TryParse<Toppings>(toppingInput.ToLower(), out var toppingType))
            {
                throw new ArgumentException($"Cannot place {toppingInput} on top of your pizza.");
            }
            topping = toppingType;
            SetToppingCalories(toppingInput.ToLower());
            this.weight = ValidateWeigth(weight, toppingInput);

        }

        public double Calories { get { return CalculateCalories(); } }

        private double CalculateCalories()
        {
            return BaseCaloriesPerGram * weight * toppingCalories;
        }

        private void SetToppingCalories(string toppingInput)
        {
            if (toppingInput == "meat")
            {
                toppingCalories = 1.2;
            }
            else if (toppingInput == "veggies")
            {
                toppingCalories = 0.8;
            }
            else if (toppingInput == "cheese")
            {
                toppingCalories = 1.1;
            }
            else if (toppingInput == "sauce")
            {
                toppingCalories = 0.9;
            }
        }

        private double ValidateWeigth(double weight, string toppingInput)
        {
            if (weight <= 0 || weight > 50)
            {
                throw new ArgumentException($"{toppingInput} weight should be in the range [1..50]."); // moje da e greshka i da iskat s glavna
            }
            return weight;

        }

        public override string ToString()
        {
            return $"{Calories:f2}";
        }
    }
}
