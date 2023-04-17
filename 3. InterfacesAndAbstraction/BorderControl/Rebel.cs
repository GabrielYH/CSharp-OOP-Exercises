using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BorderControl
{
    public class Rebel : IBuyer
    {
        public Rebel(string name, int age, string group)
        {
            Name = name;
            Age = age;
            Group = group;
        }
        public string Name { get; }
        public string Group { get; set; }
        public int Age { get;  }
        public int Food { get; private set; }
        public void BuyFood()
        {
            Food += 5;
        }
    }
}
