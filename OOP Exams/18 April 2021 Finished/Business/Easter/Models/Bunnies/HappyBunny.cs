using System;
using System.Collections.Generic;
using System.Text;

namespace Easter.Models.Bunnies
{
    public class HappyBunny : Bunny
    {
        private const int initialEnergy = 100;
        public HappyBunny(string name) : base(name, initialEnergy)
        {
        }

        public override void Work()
        {
            Energy -= 10;
            //check dali ne trq proverka ako padne pod 0 da e 0;
        }
    }
}
