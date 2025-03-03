using System;

class HelloWorld {
    static void Main() {
        
        long x = long.Parse(Console.ReadLine()); 
        long year=x/365;
        long months=(x-(year*365))/30;
        long days=x-((year*365)+(months*30));
       Console.WriteLine($"{year} years");
       Console.WriteLine($"{months} months");
       Console.WriteLine($"{days} days");
    
    }
}
