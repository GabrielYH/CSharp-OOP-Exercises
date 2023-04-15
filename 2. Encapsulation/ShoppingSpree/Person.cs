using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingSpree
{
    public class Person
    {
        private string name;
        private decimal money;
        private List<Product> bag;
        public Person(string name, decimal money)
        {
            Name = name;
            Money = money;
            bag = new();
        }
        public IReadOnlyCollection<Product> Bag { get { return this.bag.AsReadOnly(); } }
        public string Name
        {
            get { return this.name; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Name cannot be empty");
                }
                this.name = value;
            }
        }
        public decimal Money
        {
            get { return this.money; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Money cannot be negative");
                }
                this.money = value;
            }
        }

        public void PurchaseProduct(Product product)
        {
            if (Money < product.Cost)
            {
                throw new ArgumentException($"{Name} can't afford {product.Name}");
            }
            bag.Add(product);
            Money -= product.Cost;
            Console.WriteLine($"{Name} bought {product.Name}");
        }
    }
}
