﻿using Farm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingeInheritance
{
    public class Cat : Animal
    {
        public void Meow()
        {
            Console.WriteLine($"meowing…");
        }
    }
}
