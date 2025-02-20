using System;
class HelloWorld {
  static void Main() {
    
    string[] inputs = Console.ReadLine().Split(); 
    long a = Convert.ToInt64(inputs[0]);
    long b = Convert.ToInt64(inputs[1]);
    long c = Convert.ToInt64(inputs[2]);
    
     long min = Math.Min(a, Math.Min(b, c));
    long max = Math.Max(a, Math.Max(b, c)); 
    Console.WriteLine($"{min} {max}");
}}