using System;

public class Automapper
{
    public static void Map<TSource, TDestination>(TSource source, TDestination destination)
    {
        var sourceProps = typeof(TSource).GetProperties();
        var destProps = typeof(TDestination).GetProperties();

        foreach (var sProp in sourceProps)
        {
            foreach (var dProp in destProps)
            {
                if (sProp.Name == dProp.Name)
                {
                    var value = sProp.GetValue(source);
                    dProp.SetValue(destination, value);
                    break;
                }
            }
        }
    }
}


public class Source
{
    public int Id { get; set; }
    public string Name { get; set; }
}

public class Destination
{
    public int Id { get; set; }
    public string Name { get; set; }
}

class Program
{
    static void Main()
    {
        var src = new Source { Id = 1, Name = "Alice" };
        var dest = new Destination();

        Automapper.Map(src, dest);

        Console.WriteLine($"Id_dest: {dest.Id}, Name_dest: {dest.Name}");  
    }
}
