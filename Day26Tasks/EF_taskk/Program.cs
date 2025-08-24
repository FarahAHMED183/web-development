using System;
using Microsoft.EntityFrameworkCore;
using Data;
using Models;

class Program
{
    static async Task Main()
    {
        using var context = new AppDbContext();

        // ---------------------------
        // CATEGORY CRUD
        // ---------------------------
        var category = new Category { Name = "Electronics" };
        context.Categories.Add(category);
        await context.SaveChangesAsync();
        Console.WriteLine($"Created Category ID: {category.Id}");

        // ---------------------------
        // READ CATEGORY
        // ---------------------------
        var readCat = await context.Categories.FindAsync(category.Id);
        Console.WriteLine($"Read Category: {readCat?.Name}");

        // ---------------------------
        // UPDATE CATEGORY
        // ---------------------------
        readCat!.Name = "Gadgets";
        await context.SaveChangesAsync();
        Console.WriteLine($"Updated Category: {readCat.Name}");

        // ---------------------------
        // PRODUCT CRUD
        // ---------------------------
        var product = new Product
        {
            Name = "Laptop",
            Price = 999.99m,
            CategoryId = category.Id
        };
        context.Products.Add(product);
        await context.SaveChangesAsync();
        Console.WriteLine($"Created Product ID: {product.Id}");

        var readProd = await context.Products.Include(p => p.Category)
            .FirstOrDefaultAsync(p => p.Id == product.Id);
        Console.WriteLine($"Read Product: {readProd?.Name} in Category: {readProd?.Category?.Name}");

        // ---------------------------
        // CLEANUP
        // ---------------------------
        context.Products.Remove(readProd!);
        context.Categories.Remove(category);
        await context.SaveChangesAsync();
        Console.WriteLine("Deleted Product and Category");
    }
}