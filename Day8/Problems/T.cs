using System;
using System.Linq;

class Program
{
    static void Main()
    {
        string[] input = Console.ReadLine().Split(' ');
        double[] nums = Array.ConvertAll(input, double.Parse);

        
        Array.Sort(nums);
        foreach (var num in nums)
        {
            Console.WriteLine(num);
        }

        
        Console.WriteLine();

        
        foreach (var num in input)
        {
            Console.WriteLine(num);
        }
    }
}
