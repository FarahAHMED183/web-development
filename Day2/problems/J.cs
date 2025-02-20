using System;
class HelloWorld {
  static void Main() {
    
    string[] inputs = Console.ReadLine().Split(); 
    long a = Convert.ToInt64(inputs[0]);
    long b = Convert.ToInt64(inputs[1]);
    
     if (a % b==0 || b%a==0 ) {
        Console.WriteLine("Multiples");
    }
    else 
        Console.WriteLine("No Multiples");
     
  }
}