using System.Data;

namespace LibraryManager.Repositories
{
    public interface IDatabaseProvider
    {
        IDbConnection GetConnection();
    }
}