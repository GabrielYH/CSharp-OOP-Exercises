using System;
using System.Collections.Generic;
using System.Text;

namespace Easter.Models.Bunnies
{
    public class SleepyBunny : Bunny
    {
        private const int initialEnergy = 50;
        public SleepyBunny(string name) : base(name, initialEnergy)
        {
        }

        public override void Work()
        {
            Energy -= 15;
            //check dali ne trq proverka ako padne pod 0 da e 0;
        }
    }
}
