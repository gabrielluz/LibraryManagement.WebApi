using System.Data;

namespace LibraryManager.Api.Repositories.Providers
{
    public interface IDatabaseProvider
    {
        IDbConnection GetConnection();
    }
}