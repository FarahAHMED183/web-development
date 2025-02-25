using System;

class Program
{
    static void Main()
    {
        char X = Console.ReadLine()[0];

       
        if (char.IsUpper(X))
        {
            Console.WriteLine(char.ToLower(X));
        }
        else 
        {
           
            Console.WriteLine(char.ToUpper(X));
        }
    }
}
