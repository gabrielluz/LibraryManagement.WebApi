using System.Data;

namespace LibraryManager.Api.Repositories
{
    public interface IDatabaseProvider
    {
        IDbConnection GetConnection();
    }
}