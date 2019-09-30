using Dapper;
using LibraryManager.Api.Models;
using LibraryManager.Api.Models.Entities;
using LibraryManager.Api.Repositories.Interfaces;
using LibraryManager.Api.Repositories.Providers;
using System.Collections.Generic;

namespace LibraryManager.Api.Repositories.Implementations
{
    public class BooksRepository : IBooksRepository
    {
        private readonly ICrudRepository _crudRepository;
        private readonly IDatabaseProvider _databaseProvider;

        public BooksRepository(ICrudRepository crudRepository, IDatabaseProvider databaseProvider)
        {
            _crudRepository = crudRepository;
            _databaseProvider = databaseProvider;
        }

        public IEnumerable<Book> GetAllPaginated(Pagination paginationFilter)
        {
            using (var connection = _databaseProvider.GetConnection())
            {
                var parameters = new
                {
                    Limit = paginationFilter.Limit,
                    OffSet = paginationFilter.CalculateOffSet()
                };

                string sql = @"SELECT * FROM Book
                            LIMIT @Limit
                            OFFSET @OffSet;";

                return connection.Query<Book>(sql, parameters);
            }
        }

        public void Delete(long id) => _crudRepository.Delete<Book>(id);

        public Book Get(long id) => _crudRepository.Get<Book>(id);

        public Book Insert(Book book) => _crudRepository.Insert(book);

        public Book Update(Book book) => _crudRepository.Update(book);
    }
}
