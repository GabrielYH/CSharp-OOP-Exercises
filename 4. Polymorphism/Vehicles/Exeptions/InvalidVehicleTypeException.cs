using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vehicles.Exeptions
{
    public class InvalidVehicleTypeException : Exception
    {
        private const string DefaultMessage = "Vehicle type not supported!";

        //Compile-Time Polymorphism
        public InvalidVehicleTypeException()
            : base(DefaultMessage)
        {

        }

        public InvalidVehicleTypeException(string message)
            : base(message)
        {

        }
    }
}
