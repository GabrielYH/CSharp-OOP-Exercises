﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomRandomList
{
    public class RandomList : List<string>
    {
        public string RandomString()
        {
            Random random = new();
            int index = random.Next(0, this.Count);
            string removedString = this[index];
            this.RemoveAt(index);
            return removedString;

        }
    }
}
