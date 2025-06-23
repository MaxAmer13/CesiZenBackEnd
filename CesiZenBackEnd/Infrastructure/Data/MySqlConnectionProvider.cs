using MySqlConnector;
using System.Data;

namespace CesiZenBackEnd.Infrastructure.Data;

public class MySqlConnectionProvider
{
    private readonly string _connectionString;

    public MySqlConnectionProvider(string connectionString)
    {
        _connectionString = connectionString;
    }
    public IDbConnection Create()
    {
        return new MySqlConnection(_connectionString);
    }
}