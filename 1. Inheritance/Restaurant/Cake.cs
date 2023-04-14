using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant
{
    public class Cake : Dessert
    {
        private const double grams = 500;
        private const decimal cakePrice = 5;
        private const double calories = 1000;
        public Cake(string name) : base(name, cakePrice, grams, calories)
        {
                                   
        }
        
    }
}
