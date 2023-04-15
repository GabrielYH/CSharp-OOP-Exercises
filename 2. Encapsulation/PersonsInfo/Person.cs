using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonsInfo
{
    public class Person
    {
        private string firstName;
        private string lastName;
        private int age;
        private decimal salary;
        public Person(string firstName, string lastName, int age, decimal salary)
        {
            FirstName = firstName;
            LastName = lastName;
            Age = age;
            Salary = salary;
        }
        public string FirstName
        {
            get
            {
                return this.firstName;
            }
            set
            {
                if (value.Length < 3) throw new ArgumentException("First name cannot contain fewer than 3 symbols!");
                firstName = value; //
            }
        }
        public string LastName
        {
            get
            {
                return this.lastName;
            }
            set
            {
                if (value.Length < 3) throw new ArgumentException("Last name cannot contain fewer than 3 symbols!");
                lastName = value; //
            }
        }

        public int Age
        {
            get
            {
                return this.age;
            }
            set
            {
                if (value <= 0) throw new ArgumentException("Age cannot be zero or a negative integer!");
                this.age = value; //
            }
        }
        public int MyProperty { get; set; }
        public decimal Salary
        {
            get
            {
                return this.salary;
            }
            set
            {
                if (value < 650) throw new ArgumentException("Salary cannot be less than 650 leva!");
                    this.salary = value;
            }
        }

        public override string ToString()
        {
            return $"{FirstName} {LastName} receives {Salary:f2} leva.";
        }
        public void IncreaseSalary(decimal percentage)
        {
            decimal increase = 0;
            if (Age < 30)
            {
                increase += (Salary * percentage / 100) / 2;
                Salary += increase;
            }
            else if (Age >= 30)
            {
                increase += Salary * percentage / 100;
                Salary += increase;
            }
        }
    }
}
