using System;
using System.Collections.Generic;
namespace CustomStack
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            StackOfStrings stack = new();
            Console.WriteLine(stack.IsEmpty());
            List<string> elements = new() { "pesho", "gosho", "cigomigo" };
            stack.AddRange(elements);
            Console.WriteLine(stack.IsEmpty());

        }
    }
}