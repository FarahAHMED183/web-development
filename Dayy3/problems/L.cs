
using System;
class HelloWorld {
  static void Main() {
    
    
   string[] person1 = Console.ReadLine().Split();
   string[] person2 = Console.ReadLine().Split();
   
   if(person1[1]==person2[1])
   {
       Console.WriteLine("ARE Brothers");
   }
   else
   {
       Console.WriteLine("NOT");
       
   }
  }
}