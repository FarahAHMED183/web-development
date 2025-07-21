using System;
using System.Collections.Generic;

public class Person
{
    public string Name { get; set; }
    public string Gender { get; set; } 
}

public class Program
{
    
    public static string GetFirstPerson(Queue<Person> people, Func<Person, bool> condition)
    {
        foreach (var person in people)
        {
            if (condition(person))
            {
                return person.Name; 
            }
        }
        return "No match found";
    }

    public static void Main(string[] args)
    {
        Queue<Person> people = new Queue<Person>();
        people.Enqueue(new Person { Name = "farah", Gender = "Female" });
        people.Enqueue(new Person { Name = "ahmed", Gender = "Male" });
        people.Enqueue(new Person { Name = "omar", Gender = "Male" });

       
        string firstMale = GetFirstPerson(people, p => p.Gender == "Male");
        Console.WriteLine(firstMale); 
    }
}
