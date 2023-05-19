namespace Shapes
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            Shape circle = new Circle(3.5);
            Shape rectangle = new Rectangle(4, 8);
            Console.WriteLine(rectangle.Draw());
            Console.WriteLine(rectangle.CalculatePerimeter());
            Console.WriteLine(rectangle.CalculateArea());

            Console.WriteLine(circle.Draw());
            Console.WriteLine(circle.CalculatePerimeter());
            Console.WriteLine(circle.CalculateArea());
        }
    }
}