using System.Data;
using Npgsql;

namespace Discount.API.DbContext;

public class DapperContext
{
    private readonly IConfiguration _configuration;
    private readonly string _connectionString;

    public DapperContext(IConfiguration configuration)
    {
        _configuration = configuration;
        _connectionString = _configuration.GetValue<string>("DatabaseSettings:ConnectionString");
    }
    
    public NpgsqlConnection CreateNpgSqlConnection()
    {
        var connection = new NpgsqlConnection(_connectionString);
        connection.Open();
        return connection;
    }
}