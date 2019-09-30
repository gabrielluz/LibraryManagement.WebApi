using Dapper;
using LibraryManager.Api.Models;
using LibraryManager.Api.Models.Entities;
using LibraryManager.Api.Repositories.Interfaces;
using LibraryManager.Api.Repositories.Providers;
using System.Collections.Generic;

namespace LibraryManager.Api.Repositories.Implementations
{
    public class UsersRepository : IUsersRepository
    {
        private readonly ICrudRepository _crudRepository;
        private IDatabaseProvider _databaseProvider;

        public UsersRepository(ICrudRepository crudRepository, IDatabaseProvider databaseProvider)
        {
            _crudRepository = crudRepository;
            _databaseProvider = databaseProvider;
        }

        public IEnumerable<User> GetAllPaginated(Pagination paginationFilter)
        {
            using (var connection = _databaseProvider.GetConnection())
            {
                var parameters = new
                {
                    Limit = paginationFilter.Limit,
                    OffSet = paginationFilter.CalculateOffSet()
                };

                string sql = @"SELECT * FROM User
                            LIMIT @Limit
                            OFFSET @OffSet;";

                return connection.Query<User>(sql, parameters);
            }
        }

        public void Delete(long id) => _crudRepository.Delete<User>(id);

        public User Get(long id) => _crudRepository.Get<User>(id);

        public User Insert(User user) => _crudRepository.Insert(user);

        public User Update(User user) => _crudRepository.Update(user);
    }
}
