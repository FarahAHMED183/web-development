using System;
using System.Collections.Generic;

public class Person
{
    public string Name { get; set; }
    public string Gender { get; set; } 
}

// Extension method on Queue<Person>
public static class QueueExtensions
{
    public static string GetFirstPersonName(this Queue<Person> people, Func<Person, bool> condition)
    {
        foreach (var person in people)
        {
            if (condition(person))
                return person.Name;
        }
        return "No match found";
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        Queue<Person> people = new Queue<Person>();
        people.Enqueue(new Person { Name = "farah", Gender = "Female" });
        people.Enqueue(new Person { Name = "ahmed", Gender = "Male" });
        people.Enqueue(new Person { Name = "omar",  Gender = "Male" });

        // Use the extension method
        string firstMale = people.GetFirstPersonName(p => p.Gender == "Male");
        Console.WriteLine(firstMale); // Output: ahmed
    }
}
