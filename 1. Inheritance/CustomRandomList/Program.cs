using System;
using System.Collections.Generic;
using System.Linq;
namespace CustomRandomList
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            RandomList list = new();
            list.Add("Edno");
            list.Add("Dve");
            list.Add("Tri");
            list.Add("Chetiri");
            list.Add("Pet");
            Console.WriteLine(list.RandomString());
            Console.WriteLine(list.RandomString());
            

        }
    }
}