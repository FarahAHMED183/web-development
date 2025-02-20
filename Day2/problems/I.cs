using System;
class HelloWorld {
  static void Main() {
    
    string[] inputs = Console.ReadLine().Split(); 
    long a = Convert.ToInt64(inputs[0]);
    long b = Convert.ToInt64(inputs[1]);
    
     if (a >= b ) {
        Console.WriteLine("Yes");
    }
    else 
        Console.WriteLine("No");
     
  }
}