using System;
using System.Linq;
using System.Runtime.InteropServices;

namespace Telephony
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            string[] phoneNumbers = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);
            string[] websites = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);
            IStationaryPhone stationaryPhone = new StationaryPhone();
            ISmartphone smartphone = new Smartphone();

            Calling(phoneNumbers, stationaryPhone, smartphone);
            Browsing(websites, smartphone);
        }

        public static void Calling(string[] collection, IStationaryPhone stationaryPhone, ISmartphone smartphone)
        {
            foreach (var number in collection)
            {
                try
                {
                    if (number.Length == 7)
                    {
                        stationaryPhone.DialNumber(number);
                    }
                    else if (number.Length == 10)
                    {
                        smartphone.CallNumber(number);
                    }
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.Message);
                }
            }
        }

        public static void Browsing(string[] websites, ISmartphone smartphone)
        {
            foreach (var url in websites)
            {
                try
                {
                    smartphone.BrowseWeb(url);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}