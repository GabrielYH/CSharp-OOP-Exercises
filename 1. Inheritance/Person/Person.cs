using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Person
{
    public class Person
    {

        
        private int age;
        public Person(string name, int age)
        {
            Name = name;
            Age = age;

        }
        public  string Name { get; set; }
        public  int Age
        {
            get
            {
                return age;
            }

            set
            {
                if (value < 0)
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
