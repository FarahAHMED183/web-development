
 using System;

namespace HelloWorld
{
    class Program
    {
        static void Main(string[] args)
        {
            long x1, x2, x3, x4;
            string[] input = Console.ReadLine().Split();
   
            x1 = Convert.ToInt64(input[0]);
            x2 = Convert.ToInt64(input[1]);
            x3 = Convert.ToInt64(input[2]);
            x4 = Convert.ToInt64(input[3]);
            
            double Difference = (double)x1 * x2 - (double)x3 * x4;
            Console.WriteLine($"Difference = {Difference:F0}");
        }
    }
}
