using Microsoft.Data.SqlClient;
using System.Data;

namespace AuctoresOnline.API.Data;

public interface IDbConnectionFactory
{
    IDbConnection CreateConnection();
}

public class DbConnectionFactory(IConfiguration configuration) : IDbConnectionFactory
{
    private readonly string _connectionString = configuration.GetConnectionString("DefaultConnection")
        ?? throw new InvalidOperationException("DefaultConnection not configured.");

    public IDbConnection CreateConnection() => new SqlConnection(_connectionString);
}
