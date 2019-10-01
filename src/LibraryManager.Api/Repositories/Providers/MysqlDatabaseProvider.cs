using Dapper;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace LibraryManager.Api.Repositories.Providers
{
    public class MysqlDatabaseProvider : IDatabaseProvider
    {
        private const string ConnectionStringName = "LibraryManagerApiConnection";
        private readonly string _connectionString;

        public MysqlDatabaseProvider(IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString(ConnectionStringName);
            
            if (connectionString == null)
                throw new ApplicationException($"Connection string {ConnectionStringName} was not found.");

            _connectionString = connectionString;
        }

        public IDbConnection GetConnection()
        {
            return new MySqlConnection(_connectionString);
        }

        public void AssertConnectivity()
        {
            using (var connection = GetConnection())
                connection.Query<int>("SELECT (1) FROM User;");
        }
    }
}