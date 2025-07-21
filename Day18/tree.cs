using System;

public class TreeNode
{
    public int Value;
    public TreeNode Left;
    public TreeNode Right;

    public TreeNode(int value)
    {
        Value = value;
    }
}

public class Program
{
    public static int GetSmallest(TreeNode root)
    {
        if (root == null)
            throw new ArgumentException("Tree is empty");

        int smallest = root.Value;

        if (root.Left != null)
        {
            int leftSmallest = GetSmallest(root.Left);
            if (leftSmallest < smallest)
                smallest = leftSmallest;
        }

        if (root.Right != null)
        {
            int rightSmallest = GetSmallest(root.Right);
            if (rightSmallest < smallest)
                smallest = rightSmallest;
        }

        return smallest;
    }

    public static void Main()
    {
        TreeNode root = new TreeNode(10);
        root.Left = new TreeNode(5);
        root.Right = new TreeNode(15);
        root.Left.Left = new TreeNode(2);
        root.Left.Right = new TreeNode(7);

        int smallestValue = GetSmallest(root);
        Console.WriteLine($"Smallest value: {smallestValue}");  
    }
}
