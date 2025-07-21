using System;
using System.Collections.Generic;

class GraphNode
{
    public int Value;
    public List<GraphNode> Neighbors = new List<GraphNode>();

    public GraphNode(int value)
    {
        Value = value;
    }
}

class Graph
{
    private Dictionary<int, GraphNode> nodes = new Dictionary<int, GraphNode>();

    public void AddNode(int value)
    {
        if (!nodes.ContainsKey(value))
            nodes[value] = new GraphNode(value);
    }

    public void DeleteNode(int value)
    {
        if (!nodes.ContainsKey(value)) return;

        // Remove this node from neighbors' lists
        foreach (var node in nodes.Values)
        {
            node.Neighbors.RemoveAll(n => n.Value == value);
        }

        // Remove the node itself
        nodes.Remove(value);
    }

    public void AddEdge(int from, int to)
    {
        AddNode(from);
        AddNode(to);

        if (!nodes[from].Neighbors.Exists(n => n.Value == to))
            nodes[from].Neighbors.Add(nodes[to]);

        if (!nodes[to].Neighbors.Exists(n => n.Value == from))
            nodes[to].Neighbors.Add(nodes[from]);
    }

    public void DeleteEdge(int from, int to)
    {
        if (nodes.ContainsKey(from))
            nodes[from].Neighbors.RemoveAll(n => n.Value == to);

        if (nodes.ContainsKey(to))
            nodes[to].Neighbors.RemoveAll(n => n.Value == from);
    }

    public void Print()
    {
        foreach (var node in nodes.Values)
        {
            Console.Write(node.Value + ": ");
            foreach (var neighbor in node.Neighbors)
                Console.Write(neighbor.Value + " ");
            Console.WriteLine();
        }
    }
}

class Program
{
    static void Main()
    {
        var graph = new Graph();

        graph.AddNode(1);
        graph.AddNode(2);
        graph.AddNode(3);

        graph.AddEdge(1, 2);
        graph.AddEdge(2, 3);
        graph.AddEdge(1, 3);

        Console.WriteLine("Graph after adding nodes and edges:");
        graph.Print();

        graph.DeleteEdge(1, 3);
        Console.WriteLine("\nGraph after deleting edge between 1 and 3:");
        graph.Print();

    }
}
