using System;

namespace HelloWorld
{
  class Program
  {
    static void Main(string[] args)
    {
        string[] inputs = Console.ReadLine().Split();

        int x = Convert.ToInt32(inputs[0]);
        long l = Convert.ToInt64(inputs[1]);
        char ch = Convert.ToChar(inputs[2]);
        float f = Convert.ToSingle(inputs[3]);
        double d = Convert.ToDouble(inputs[4]);

        Console.WriteLine(x);
        Console.WriteLine(l);
        Console.WriteLine(ch);
        Console.WriteLine(f);
        Console.WriteLine(d);
    }
  }
}