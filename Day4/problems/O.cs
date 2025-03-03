/******************************************************************************

                            Online C# Compiler.
                Code, Compile, Run and Debug C# program online.
Write your code in this editor and press "Run" button to execute it.

*******************************************************************************/

using System;
class HelloWorld {
  static void Main() {

        string input = Console.ReadLine().Replace(" ", "");

        
        int opIndex = input.IndexOfAny(new char[] { '+', '-', '*', '/' });

        if (opIndex == -1)
            return;

        long x = long.Parse(input.Substring(0, opIndex));
        char ch = input[opIndex];
        long y = long.Parse(input.Substring(opIndex + 1));
        long result;
      if(ch=='+')
      {
          Console.WriteLine(x+y);
      }
      else if(ch=='-')
      {
          Console.WriteLine(x-y);
      }
      else if(ch=='*')
      {
          Console.WriteLine(x*y);
      }
      else if(ch=='/')
      {
          if(y==0)
          {
              return;
          }
          result=x/y;
          Console.WriteLine((int)result);
      }
      else
            return;
    
  }
}