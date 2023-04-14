using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Person
{
    public class Child
    {
        private int age;
        public Child(string name, int age)
        {
            Name = name;
            Age = age;
        }
        public string Name { get; set; }
        public  int Age
        {
            get
            {
                return this.age;
            }

            set
            {
                if (value > 15 || value < 0 )
                {
                    throw new ArgumentException();
                }
                this.age = value;
            }
        }

        public override string ToString()
        {
            return $"Name: {Name}, Age: {Age}";
        }


    }
}
