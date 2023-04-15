using System;
using System.Collections.Generic;
using System.Linq;

namespace ShoppingSpree
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            List<Person> people = new();
            List<Product> products = new();
            try
            {

                string[] peopleInput = Console.ReadLine().Split(";", StringSplitOptions.RemoveEmptyEntries);

                foreach (var item in peopleInput)
                {
                    string[] personAndMoney = item.Split("=", StringSplitOptions.RemoveEmptyEntries);

                    Person person = new(personAndMoney[0], decimal.Parse(personAndMoney[1]));
                    people.Add(person);
                }

                string[] productInput = Console.ReadLine().Split(";", StringSplitOptions.RemoveEmptyEntries);
                foreach (var item in productInput)
                {
                    string[] productAndCost = item.Split("=", StringSplitOptions.RemoveEmptyEntries);


                    Product product = new(productAndCost[0], decimal.Parse(productAndCost[1]));
                    products.Add(product);
                }
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }


            string command;
            while ((command = Console.ReadLine()) != "END")
            {
                string[] cmdArgs = command.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                string personName = cmdArgs[0];
                string product = cmdArgs[1];

                if (products.Any(x => x.Name == product) && people.Any(x => x.Name == personName))
                {
                    Person currPerson = people.FirstOrDefault(x => x.Name == personName);
                    Product currProduct = products.FirstOrDefault(x => x.Name == product);
                    try
                    {
                        currPerson.PurchaseProduct(currProduct);

                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }


            }


            foreach (var person in people)
            {

                if (person.Bag.Count != 0)
                {
                    Console.WriteLine($"{person.Name} - {string.Join(", ", person.Bag)}");
                }
                else
                {
                    Console.WriteLine($"{person.Name} - Nothing bought");
                }
            }
        }
    }
}