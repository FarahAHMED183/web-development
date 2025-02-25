using System;

class Program
{
    static void Main()
    {
        char X = Console.ReadLine()[0];

       
        if (char.IsDigit(X))
        {
            Console.WriteLine("IS DIGIT");
        }
        else if (char.IsLetter(X)) 
        {
            Console.WriteLine("ALPHA");
            if (char.IsUpper(X))
            {
                Console.WriteLine("IS CAPITAL");
            }
            else
            {
                Console.WriteLine("IS SMALL");
            }
        }
    }
}
