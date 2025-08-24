using System;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.SqlClient;
using Models;

class Program
{
    static async Task Main()
    {
        string connectionString = "Server=DESKTOP-5SMF0GV;Database=Dapper_Task;Trusted_Connection=True;TrustServerCertificate=True;";

        await using var connection = new SqlConnection(connectionString);
        await connection.OpenAsync();

        Console.WriteLine("=== CATEGORY OPERATIONS ===");
        int categoryId = await CreateCategory(connection, "Electronics");
        await ShowCategory(connection, categoryId);
        await UpdateCategory(connection, categoryId, "Gadgets");
        await ListAllCategories(connection);

        Console.WriteLine("\n=== PRODUCT OPERATIONS ===");
        int productId = await CreateProduct(connection, "Laptop", 999.99m, categoryId);
        await ShowProduct(connection, productId);
        await UpdateProduct(connection, productId, "Gaming Laptop", 1199.99m, categoryId);
        await ListAllProducts(connection);

        Console.WriteLine("\n=== PRODUCTS WITH CATEGORIES ===");
        await ShowProductsWithCategories(connection, categoryId);

        Console.WriteLine("\n=== TOTAL PRICE BY CATEGORY ===");
        await ShowTotalPriceByCategory(connection, categoryId);

        Console.WriteLine("\n=== CLEANUP ===");
        await DeleteProduct(connection, productId);
        await DeleteCategory(connection, categoryId);
    }

    // --- CATEGORY METHODS ---
    static async Task<int> CreateCategory(SqlConnection conn, string name)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@Name", name, DbType.String);
        parameters.Add("@NewCategoryId", dbType: DbType.Int32, direction: ParameterDirection.Output);

        await conn.ExecuteAsync("dbo.usp_CreateCategory", parameters, commandType: CommandType.StoredProcedure);
        int id = parameters.Get<int>("@NewCategoryId");
        Console.WriteLine($"Created Category ID: {id}");
        return id;
    }

    static async Task ShowCategory(SqlConnection conn, int id)
    {
        var category = await conn.QueryFirstOrDefaultAsync<Category>(
            "dbo.usp_GetCategoryById",
            new { Id = id },
            commandType: CommandType.StoredProcedure
        );
        Console.WriteLine($"Category: {category?.Name}");
    }

    static async Task UpdateCategory(SqlConnection conn, int id, string newName)
    {
        await conn.ExecuteAsync(
            "dbo.usp_UpdateCategory",
            new { Id = id, Name = newName },
            commandType: CommandType.StoredProcedure
        );
        Console.WriteLine($"Updated Category ID: {id} -> {newName}");
    }

    static async Task ListAllCategories(SqlConnection conn)
    {
        var all = await conn.QueryAsync<Category>("dbo.usp_GetAllCategories", commandType: CommandType.StoredProcedure);
        foreach (var c in all)
            Console.WriteLine($"ID: {c.Id}, Name: {c.Name}");
    }

    static async Task DeleteCategory(SqlConnection conn, int id)
    {
        await conn.ExecuteAsync("dbo.usp_DeleteCategory", new { Id = id }, commandType: CommandType.StoredProcedure);
        Console.WriteLine($"Deleted Category ID: {id}");
    }

    // --- PRODUCT METHODS ---
    static async Task<int> CreateProduct(SqlConnection conn, string name, decimal price, int categoryId)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@Name", name, DbType.String);
        parameters.Add("@Price", price, DbType.Decimal);
        parameters.Add("@CategoryId", categoryId, DbType.Int32);
        parameters.Add("@NewProductId", dbType: DbType.Int32, direction: ParameterDirection.Output);

        await conn.ExecuteAsync("dbo.usp_CreateProduct", parameters, commandType: CommandType.StoredProcedure);
        int id = parameters.Get<int>("@NewProductId");
        Console.WriteLine($"Created Product ID: {id}");
        return id;
    }

    static async Task ShowProduct(SqlConnection conn, int id)
    {
        var product = await conn.QueryFirstOrDefaultAsync<Product>(
            "dbo.usp_GetProductById",
            new { Id = id },
            commandType: CommandType.StoredProcedure
        );
        Console.WriteLine($"Product: {product?.Name}, Price: ${product?.Price}");
    }

    static async Task UpdateProduct(SqlConnection conn, int id, string name, decimal price, int categoryId)
    {
        await conn.ExecuteAsync(
            "dbo.usp_UpdateProduct",
            new { Id = id, Name = name, Price = price, CategoryId = categoryId },
            commandType: CommandType.StoredProcedure
        );
        Console.WriteLine($"Updated Product ID: {id} -> {name}, ${price}");
    }

    static async Task ListAllProducts(SqlConnection conn)
    {
        var all = await conn.QueryAsync<Product>("dbo.usp_GetAllProducts", commandType: CommandType.StoredProcedure);
        foreach (var p in all)
            Console.WriteLine($"ID: {p.Id}, Name: {p.Name}, Price: ${p.Price}");
    }

    static async Task DeleteProduct(SqlConnection conn, int id)
    {
        await conn.ExecuteAsync("dbo.usp_DeleteProduct", new { Id = id }, commandType: CommandType.StoredProcedure);
        Console.WriteLine($"Deleted Product ID: {id}");
    }

    // --- VIEW & FUNCTION ---
    static async Task ShowProductsWithCategories(SqlConnection conn, int categoryId)
    {
        var results = await conn.QueryAsync<ProductWithCategory>(
            "SELECT * FROM dbo.vw_ProductsWithCategories WHERE CategoryId = @CategoryId",
            new { CategoryId = categoryId }
        );
        foreach (var item in results)
            Console.WriteLine($"Product: {item.ProductName}, Category: {item.CategoryName}, Price: ${item.Price}");
    }

    static async Task ShowTotalPriceByCategory(SqlConnection conn, int categoryId)
    {
        var total = await conn.ExecuteScalarAsync<decimal>(
            "SELECT dbo.fn_TotalPriceByCategory(@CategoryId)",
            new { CategoryId = categoryId }
        );
        Console.WriteLine($"Total Price in Category {categoryId}: ${total}");
    }
}
