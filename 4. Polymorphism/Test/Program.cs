using System;
using System.Collections.Generic;
using System.Linq;
using Test.Factories;
using Test.Factories.Interfaces;
using Test.Models;
using Test.Models.Interfaces;

namespace Test
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            IVehicleFactory factory = new VehicleFactory();
            ICollection<IVehicle> vehicles = new List<IVehicle>();



            for (int i = 0; i < 3; i++)
            {
                string[] vehicleData = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);
                IVehicle vehicle = factory.CreateVehicle(vehicleData[0], double.Parse(vehicleData[1]), double.Parse(vehicleData[2]), double.Parse(vehicleData[3]));
                vehicles.Add(vehicle);
            }

            int n = int.Parse(Console.ReadLine());
            for (int i = 0; i < n; i++)
            {
                try
                {

                
                string[] cmdArgs = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);
                IVehicle vehicle = vehicles.FirstOrDefault(x => x.GetType().Name == cmdArgs[1]);
                if (cmdArgs[0] == "Drive")
                {
                    Console.WriteLine(vehicle.Drive(double.Parse(cmdArgs[2])));
                }
                else if (cmdArgs[0] == "Refuel")
                {
                    vehicle.Refuel(double.Parse(cmdArgs[2]));
                }
                else if (cmdArgs[0] == "DriveEmpty")
                {
                    IVehicle bus = vehicles.FirstOrDefault(x => x.GetType().Name == "Bus");
                    Console.WriteLine(((Bus)bus).DriveEmpty(double.Parse(cmdArgs[2])));
                }
                }
                catch (ArgumentException ex)
                {

                    Console.WriteLine(ex.Message);
                }
                catch (Exception)
                {
                    throw;
                }
            }

            foreach (var item in vehicles)
            {
                Console.WriteLine(item);
            }

        }
    }
}