using System;

class Program
{
    static void Main()
    {
        string input = Console.ReadLine();

        if (input.Contains("."))
        {
            string[] parts = input.Split('.');

            
            if (int.Parse(parts[1]) == 0)
            {
                Console.WriteLine($"int {parts[0]}");
            }
            else
            {
                Console.WriteLine($"float {parts[0]} 0.{parts[1]}");
            }
        }
        else
        {
            Console.WriteLine($"int {input}");
        }
    }
}
