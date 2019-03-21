using System.Data;

namespace LibraryManagerApi.Repositories
{
    public interface IDatabaseProvider
    {
        IDbConnection GetConnection();
    }
}