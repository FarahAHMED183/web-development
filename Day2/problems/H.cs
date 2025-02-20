using System;
class HelloWorld {
  static void Main() {
    
    string[] inputs = Console.ReadLine().Split(); 
    long a = Convert.ToInt64(inputs[0]);
    long b = Convert.ToInt64(inputs[1]);
    
     if (b == 0) {
        Console.WriteLine("Error: Division by zero is not allowed.");
        return; 
    }
     double div= (double)a/b;
     double floor_Result = Math.Floor(div);
     double ceil_Result=Math.Ceiling(div);
     double round_result=Math.Round(div, MidpointRounding.AwayFromZero);
      Console.WriteLine($"floor {a} / {b} = {floor_Result}");
    Console.WriteLine($"ceil {a} / {b} = {ceil_Result}");
    Console.WriteLine($"round {a} / {b} = {round_result}");
  }
}