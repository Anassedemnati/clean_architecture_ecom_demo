using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Ordering.Infrastructure.Persistence;

public class OrderContext
{
    private readonly IConfiguration? _configuration;
    private readonly string? _connectionString;

    public OrderContext(IConfiguration? configuration)
    {
        _configuration = configuration;
        _connectionString = _configuration!.GetConnectionString("OrderingConnectionString");
    }
    public IDbConnection CreateConnection()
        => new SqlConnection(_connectionString);
}
