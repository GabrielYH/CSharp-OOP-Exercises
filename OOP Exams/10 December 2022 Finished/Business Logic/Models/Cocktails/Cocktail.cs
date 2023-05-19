using ChristmasPastryShop.Models.Cocktails.Contracts;
using ChristmasPastryShop.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChristmasPastryShop.Models.Cocktails
{
    public abstract class Cocktail : ICocktail
    {
        private string name;
        private string size;
        private double price;
        public Cocktail(string cocktailName, string size, double price)
        {
            Name = cocktailName;
            Size = size;
            Price = price;
        }
        public string Name
        {
            get => this.name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.NameNullOrWhitespace));
                }
                this.name = value;
            }
        }

        public string Size // will be validated in the controller
        {
            get => this.size;
            private set => this.size = value; //dali trq e private set
        }

        public double Price
        {
            get => this.price;
            private set
            {
                if (Size == "Large")
                {
                    this.price = value;
                }
                else if (Size == "Middle")
                {
                    this.price = ((2.0 / 3) * value);
                }
                else if (Size == "Small")
                {
                    this.price = ((1.0 / 3) * value);
                }
            }
        }

        public override string ToString()
        {
            return $"{Name} ({Size}) - {Price:f2} lv";
        }
    }
}
