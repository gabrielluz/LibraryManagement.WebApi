using MySql.Data.MySqlClient;
using System.Data;

namespace LibraryManager.Api.Repositories.Providers
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