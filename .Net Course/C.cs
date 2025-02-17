
 using System;

namespace HelloWorld
{
    class Program
    {
        static void Main(string[] args)
        {
            
            string[] input = Console.ReadLine().Split();

            long x = Convert.ToInt64(input[0]);
            long y = Convert.ToInt64(input[1]);
            
            
            Console.WriteLine($"{x} + {y} = {x + y}");
            Console.WriteLine($"{x} * {y} = {x * y}");
            Console.WriteLine($"{x} - {y} = {x - y}");
        }
    }
}
