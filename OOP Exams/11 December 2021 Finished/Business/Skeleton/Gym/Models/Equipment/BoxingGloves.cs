using System;
using System.Collections.Generic;
using System.Text;

namespace Gym.Models.Equipment
{
    public class BoxingGloves : Equipment
    {
        private const double weightOfGloves = 227;
        private const decimal priceOfGloves = 120;
        public BoxingGloves() : base(weightOfGloves, priceOfGloves)
        {
        }
    }
}
