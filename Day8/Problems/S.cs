using System;

class HelloWorld {
  static void Main() {
    // Force C# to accept dots as decimal separator
    System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

    double num = Convert.ToDouble(Console.ReadLine());

    if(num >= 0 && num <= 25)
    {
        Console.WriteLine("Interval [0,25]");
    }
    else if(num > 25 && num <= 50)
    {
        Console.WriteLine("Interval (25,50]");
    }
    else if(num > 50 && num <= 75)
    {
        Console.WriteLine("Interval (50,75]");
    }
    else if(num > 75 && num <= 100)
    {
        Console.WriteLine("Interval (75,100]");
    }
    else
    {
        Console.WriteLine("Out of Intervals");   
    }
  }
}
