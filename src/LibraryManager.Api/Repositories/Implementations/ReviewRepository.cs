using Dapper;
using LibraryManager.Api.Exceptions;
using LibraryManager.Api.Models;
using LibraryManager.Api.Models.Entities;
using LibraryManager.Api.Repositories.Interfaces;
using LibraryManager.Api.Repositories.Providers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace LibraryManager.Api.Repositories.Implementations
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly ICrudRepository _crudRepository;
        private readonly IDatabaseProvider _databaseProvider;

        public ReviewRepository(
            ICrudRepository crudRepository,
            IDatabaseProvider databaseProvider)
        {
            _crudRepository = crudRepository;
            _databaseProvider = databaseProvider;
        }

        public Review Insert(Review review)
        {
            if (review == null)
                throw new ArgumentNullException("Review must be not null.");

            _crudRepository.Get<User>(review.User.Id);
            _crudRepository.Get<Book>(review.Book.Id);

            var insertedId = 0;

            var sqlCommand = "INSERT INTO Review(Comment, Rate, UserId, BookId) "
                + "VALUES (@Comment, @Rate, @UserId, @BookId);"
                + "SELECT LAST_INSERT_ID();";

            var sqlParams = new
            {
                review.Comment,
                review.Rate,
                UserId = review.User.Id,
                BookId = review.Book.Id
            };

            using (var connection = _databaseProvider.GetConnection())
            {
                var reader = connection.ExecuteReader(sqlCommand, sqlParams);
                if (reader.Read())
                    insertedId = reader.GetInt32(0);
            }

            return Get(sqlParams.BookId, insertedId);
        }

        public Review Update(long bookId, Review review)
        {
            _crudRepository.Get<Book>(bookId);
            using (var connection = _databaseProvider.GetConnection())
            {
                var sql = "UPDATE Review SET Comment = @Comment, Rate = @Rate WHERE Id = @Id AND BookId = @BookId;";
                var sqlParameters = new
                {
                    review.Comment,
                    review.Rate,
                    review.Id,
                    bookId
                };
                connection.Execute(sql, sqlParameters);
                return Get(review.Id, connection);
            }
        }

        public IEnumerable<Review> GetAllPaginated(long bookId, Pagination paginationFilter)
        {
            _crudRepository.Get<Book>(bookId);

            using (var connection = _databaseProvider.GetConnection())
            {
                var parameters = new
                {
                    Limit = paginationFilter.Limit,
                    OffSet = paginationFilter.CalculateOffSet(),
                    BookId = bookId
                };

                string sql = @"SELECT * 
                        FROM Review r 
                        INNER JOIN User u ON r.UserId = u.Id
                        INNER JOIN Book b ON r.BookId = b.Id
                        WHERE BookId = @BookId
                        ORDER BY r.Id
                        LIMIT @Limit
                        OFFSET @OffSet;";

                var reviews = connection.Query<Review, User, Book, Review>(
                    sql,
                    (review, user, book) =>
                    {
                        review.User = user;
                        review.Book = book;
                        return review;
                    },
                    parameters);

                return reviews;
            }
        }

        public Review Get(long bookId, long reviewId)
        {
            _crudRepository.Get<Book>(bookId);

            using (var connection = _databaseProvider.GetConnection())
                return Get(reviewId, connection);
        }

        private Review Get(long reviewId, IDbConnection connection)
        {
            var queryParameter = new { Id = reviewId };

            var query = @"SELECT *
                        FROM Review r 
                        INNER JOIN User u ON r.UserId = u.Id
                        Where r.Id = @Id;";

            var review = connection
                .Query<Review, User, Review>(query, IncludeUser, queryParameter)
                .FirstOrDefault() ?? throw new EntityNotFoundException(nameof(Review), reviewId);

            return review;
        }

        public void Delete(long bookId, long id)
        {
            _crudRepository.Get<Book>(bookId);
            _crudRepository.Delete<Review>(id);
        }

        private Review IncludeUser(Review review, User user)
        {
            review.User = user;
            return review;
        }
    }
}