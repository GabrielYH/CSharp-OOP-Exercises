namespace NeedForSpeed
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            SportCar car3 = new(100, 70);
            Car car = new(200, 70);
          
            SportCar car2 = new(300, 70);
           
            car.FuelConsumption = 100;
            car2.FuelConsumption = 4;
            car3.FuelConsumption = 5;
            car.Drive(20);
            Console.WriteLine(car.Fuel);
            Console.WriteLine(car2.Fuel);
            Console.WriteLine(car3.Fuel);
            
        }
    }
}