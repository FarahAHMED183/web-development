using System;

class HelloWorld {
    static void Main() {
        string[] inputs = Console.ReadLine().Split(' ');
        double x = double.Parse(inputs[0]); 
        double y = double.Parse(inputs[1]);

        if (x == 0 && y == 0)
            Console.WriteLine("Origem");
        else if (x == 0)
            Console.WriteLine("Eixo Y");
        else if (y == 0)
            Console.WriteLine("Eixo X");
        else if (x > 0 && y > 0)
            Console.WriteLine("Q1");
        else if (x < 0 && y > 0)
            Console.WriteLine("Q2");
        else if (x < 0 && y < 0)
            Console.WriteLine("Q3");
        else
            Console.WriteLine("Q4");
    }
}
