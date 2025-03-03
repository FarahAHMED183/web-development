

using System;
class HelloWorld {
  static void Main() {

        long n=long.Parse(Console.ReadLine());
        long last=n/1000;
        if(last%2==0)
        {
            Console.WriteLine("EVEN");
        }
        else
            Console.WriteLine("ODD");
    
  }
}