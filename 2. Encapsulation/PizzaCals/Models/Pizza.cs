using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaCals.Models
{
    public class Pizza
    {
        private HashSet<Topping> toppings;
        private string name;
        private Dough dough;
        public Pizza(string name, Dough dough)
        {
            Name = name;
            this.dough = dough;
            toppings = new();
        }
        public string Name
        {
            get => this.name;
            set
            {
                if (string.IsNullOrWhiteSpace(value) || value.Length > 15) //
                {
                    throw new ArgumentException("Pizza name should be between 1 and 15 symbols.");
                }
                this.name = value;
            }
        }
        public IReadOnlyCollection<Topping> Toppings { get { return this.toppings; } }

        public double TotalCalories { get { return CalculateTotalCalories(); } }

        public void AddTopping(Topping topping)
        {
            if (toppings.Count == 10)
            {
                throw new ArgumentException("Number of toppings should be in range [0..10].");
            }
            toppings.Add(topping);
        }

        private double CalculateTotalCalories()
        {
            double total = 0;
            total += this.dough.Calories;
            foreach (var item in toppings)
            {
                total += item.Calories;
            }

            return total;
        }
        public override string ToString()
        {
            return $"{Name} - {TotalCalories:f2} Calories.  ";
        }
    }
}
