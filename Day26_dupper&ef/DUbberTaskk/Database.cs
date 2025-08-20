using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace DupperTask;

public class Database
{
    private readonly IConfiguration _config;

    public Database(IConfiguration config)
    {
        _config = config;
    }

    public IDbConnection CreateConnection()
    {
        var connectionString = _config.GetConnectionString("DefaultConnection");
        return new SqlConnection(connectionString);
    }
}