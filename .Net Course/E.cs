
 using System;

namespace HelloWorld
{
    class Program
    {
        static void Main(string[] args)
        {
            
            
            const double pi= 3.141592653;
            double Area;
            float r = Convert.ToSingle(Console.ReadLine());
            
   
             Area=pi*r*r;
             Console.WriteLine($"{Area:F9}");
    
        }
    }
}
