using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telephony
{
    public class Smartphone : ISmartphone
    {
        public void BrowseWeb(string url)
        {
            bool containsNonLetters = url.Any(x=> char.IsDigit(x));
            if (containsNonLetters)
            {
                throw new ArgumentException("Invalid URL!");
            }
            Console.WriteLine($"Browsing: {url}!");
        }

        public void CallNumber(string number)
        {
            bool containsNonDigit = number.Any(x => !char.IsDigit(x));
            if (containsNonDigit)
            {
                throw new ArgumentException("Invalid number!");
            }
            Console.WriteLine($"Calling... {number}");
        }
    }
}
