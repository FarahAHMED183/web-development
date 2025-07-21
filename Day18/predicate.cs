using System;
using System.Collections.Generic;

public class Person
{
    public string Name { get; set; }
    public string Gender { get; set; }
}

public class Program
{
    public static Person GetFirst(Stack<Person> stack, Predicate<Person> condition)
    {
        foreach (var person in stack)
        {
            if (condition(person))
            {
                return person;
            }
        }
        return null;
    }

    public static void Main(string[] args)
    {
        Stack<Person> people = new Stack<Person>();

        people.Push(new Person { Name = "Ali", Gender = "Male" });
        people.Push(new Person { Name = "Mohammed", Gender = "Male" });
        people.Push(new Person { Name = "Sara", Gender = "Female" });

        Person firstMaleMohammed = GetFirst(people, p => p.Name == "Mohammed" && p.Gender == "Male");

        if (firstMaleMohammed != null)
            Console.WriteLine($"Found: {firstMaleMohammed.Name}, Gender: {firstMaleMohammed.Gender}");
        else
            Console.WriteLine("No matching person found");
    }
}
