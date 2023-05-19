using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Models.Interfaces;

namespace Test.Factories.Interfaces
{
    public interface IVehicleFactory
    {
        IVehicle CreateVehicle(string type,double fuelQuantity, double fuelConsumption, double tankCapacity);
    }
}
