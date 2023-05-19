using System;
using System.Collections.Generic;
using System.Text;

namespace Gym.Models.Equipment
{
    public class Kettlebell : Equipment
    {
        private const double weightOfKettlebell = 10000;
        private const decimal priceOfKettlebell = 80;
        public Kettlebell() : base(weightOfKettlebell, priceOfKettlebell)
        {
        }
    }
}
