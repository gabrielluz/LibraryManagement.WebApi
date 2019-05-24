using System.Data;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace LibraryManager.Api.Repositories
{
    public class MysqlDatabaseProvider : IDatabaseProvider
    {
        private readonly MySqlConnection _connection;

        public MysqlDatabaseProvider(string connectionString)
        {
            _connection = new MySqlConnection(connectionString);
        }

        public IDbConnection GetConnection()
        {
            return _connection;
        }
    }
}