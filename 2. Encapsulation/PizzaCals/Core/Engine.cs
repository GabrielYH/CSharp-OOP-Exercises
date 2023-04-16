using PizzaCals.Core.Interfaces;
using PizzaCals.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaCals.Core
{
    public class Engine : IEngine
    {
        public void Run()
        {
            try
            {
                string[] pizzaData = Console.ReadLine()
                        .Split(" ", StringSplitOptions.RemoveEmptyEntries);
                if (pizzaData.Length < 2)
                {
                    throw new ArgumentException("Pizza name should be between 1 and 15 symbols.");
                }
                string[] doughData = Console.ReadLine()
                        .Split(" ", StringSplitOptions.RemoveEmptyEntries);
                Dough dough = new(doughData[1], doughData[2], double.Parse(doughData[3]));
                Pizza pizza = new(pizzaData[1], dough);
                string command;
                while ((command = Console.ReadLine()) != "END")
                {
                    string[] toppingData = command
                        .Split(" ", StringSplitOptions.RemoveEmptyEntries);
                    Topping topping = new(toppingData[1], double.Parse(toppingData[2]));
                    pizza.AddTopping(topping);
                }
                Console.WriteLine(pizza);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
