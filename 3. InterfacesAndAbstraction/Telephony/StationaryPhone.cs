using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telephony
{
    public class StationaryPhone : IStationaryPhone
    {
        public void DialNumber(string number)
        {
            if (number.Any(x => !char.IsDigit(x)))
            {
                throw new ArgumentException("Invalid number!");
            }
            Console.WriteLine($"Dialing... {number}");
        }
    }
}
