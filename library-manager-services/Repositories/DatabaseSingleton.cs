using MySql.Data.MySqlClient;

namespace LibraryManager.Repositories
{
    public static class DatabaseSingleton 
    {
        public static MySqlConnection GetConnection() 
        {
            var connectionSB = new MySqlConnectionStringBuilder();
            connectionSB.Port = 3306;
            connectionSB.UserID = "gabrielluz";
            connectionSB.Password = "a1b2c3d4";
            connectionSB.Database = "LibraryManagement";
            connectionSB.Server = "library-management-dev.cbyofxowdiqo.sa-east-1.rds.amazonaws.com";
            return new MySqlConnection(connectionSB.GetConnectionString(true));
        }
    }
}