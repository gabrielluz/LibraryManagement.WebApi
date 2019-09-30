using Dapper;
using LibraryManager.Api.Exceptions;
using LibraryManager.Api.Models;
using LibraryManager.Api.Models.Entities;
using LibraryManager.Api.Repositories.Interfaces;
using LibraryManager.Api.Repositories.Providers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LibraryManager.Api.Repositories.Implementations
{
    public class RentalRepository : IRentalRepository
    {
        private readonly ICrudRepository _crudRepository;
        private readonly IDatabaseProvider _databaseProvider;

        public RentalRepository(ICrudRepository crudRepository,
            IDatabaseProvider databaseProvider)
        {
            _crudRepository = crudRepository;
            _databaseProvider = databaseProvider;
        }

        public Rental Insert(Rental rental)
        {
            if (rental == null)
                throw new ArgumentNullException(nameof(rental));

            _crudRepository.Get<User>(rental.User?.Id ?? 0);
            _crudRepository.Get<Book>(rental.Book?.Id ?? 0);

            string sql = "INSERT INTO Rental(IdUser, IdBook, Issued) "
                    + "VALUES(@UserId, @BookId, @Issued);"
                    + "SELECT LAST_INSERT_ID();";

            var sqlParams = new
            {
                UserId = rental.User.Id,
                BookId = rental.Book.Id,
                Issued = DateTime.Now
            };

            long insertedId = 0;

            using (var connection = _databaseProvider.GetConnection())
            {
                var reader = connection.ExecuteReader(sql, sqlParams);

                if (reader.Read())
                    insertedId = reader.GetInt32(0);
            }

            return Get(insertedId);
        }

        public Rental Update(Rental rental)
        {
            using (var conn = _databaseProvider.GetConnection())
            {
                var sql = "UPDATE Rental SET Returned = @Returned WHERE Id = @Id;";
                conn.Execute(sql, new { rental.Returned, rental.Id });
                return Get(rental.Id);
            }
        }

        public IEnumerable<Rental> GetAll()
        {
            using (var connection = _databaseProvider.GetConnection())
            {
                var query = @"SELECT *
                            FROM Rental r 
                            INNER JOIN User u ON r.IdUser = u.Id
                            INNER JOIN Book b ON r.IdBook = b.Id;";

                var rentalList = connection
                    .Query(query, (Func<Rental, User, Book, Rental>)IncludeUserAndBook)
                    .ToList();

                return rentalList;
            }
        }

        public IEnumerable<Rental> GetAllPaginated(Pagination paginationFilter)
        {
            using (var connection = _databaseProvider.GetConnection())
            {
                var parameters = new
                {
                    Limit = paginationFilter.Limit,
                    OffSet = paginationFilter.CalculateOffSet()
                };
                var query = @"SELECT *
                            FROM Rental r 
                            INNER JOIN User u ON r.IdUser = u.Id
                            INNER JOIN Book b ON r.IdBook = b.Id
                            LIMIT @Limit
                            OFFSET @OffSet;";

                var rentalList = connection
                    .Query(query, (Func<Rental, User, Book, Rental>)IncludeUserAndBook, parameters)
                    .ToList();

                return rentalList;
            }
        }

        public Rental Get(long id)
        {
            using (var connection = _databaseProvider.GetConnection())
            {
                var queryParameter = new { Id = id };

                var query = @"SELECT *
                            FROM Rental r 
                            INNER JOIN User u ON r.IdUser = u.Id
                            INNER JOIN Book b ON r.IdBook = b.Id
                            Where r.Id = @Id;";

                var rental = connection
                    .Query(query, (Func<Rental, User, Book, Rental>)IncludeUserAndBook, queryParameter)
                    .FirstOrDefault() ?? throw new EntityNotFoundException(nameof(Rental), id);

                return rental;
            }
        }

        public void Delete(long id)
        {
            _crudRepository.Delete<Rental>(id);
        }

        private Rental IncludeUserAndBook(Rental rental, User user, Book book)
        {
            rental.User = user;
            rental.Book = book;
            return rental;
        }
    }
}