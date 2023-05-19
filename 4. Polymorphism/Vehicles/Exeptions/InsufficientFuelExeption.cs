using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vehicles.Exeptions
{
    public class InsufficientFuelException : Exception
    {
                public InsufficientFuelException(string message) : base(message)
        {

        }
    }
}
