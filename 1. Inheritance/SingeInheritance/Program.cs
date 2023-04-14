using SingeInheritance;
using System;
namespace Farm
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            Dog dog = new();
            dog.Bark();
            dog.Eat();
            Puppy puppy = new();
            puppy.Weep();
            Cat cat = new();
            cat.Meow();
        }
    }
}