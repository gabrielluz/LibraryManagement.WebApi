using Dapper;
using LibraryManager.Api.Exceptions;
using LibraryManager.Api.Models;
using LibraryManager.Api.Models.Entities;
using LibraryManager.Api.Repositories.Interfaces;
using LibraryManager.Api.Repositories.Providers;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

            using (var reader = _databaseProvider.GetConnection().ExecuteReader(sqlCommand, sqlParams))
            {
                if (reader.Read())
                    insertedId = reader.GetInt32(0);
            }

            return Get(review.Book.Id, insertedId);
        }

        public Review Update(long bookId, Review review)
        {
            _crudRepository.Get<Book>(bookId);
            var sql = "UPDATE Review SET Comment = @Comment, Rate = @Rate WHERE Id = @Id AND BookId = @BookId;";
            var sqlParameters = new
            {
                review.Comment,
                review.Rate,
                review.Id,
                bookId
            };
            _databaseProvider.GetConnection().Execute(sql, sqlParameters);
            return Get(bookId, review.Id);
        }

        public IEnumerable<Review> GetAllPaginated(long bookId, Pagination paginationFilter)
        {
            _crudRepository.Get<Book>(bookId);
            try
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

                var reviews = _databaseProvider.GetConnection()
                    .Query<Review, User, Book, Review>(
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
            catch (SqlException ex)
            {
                throw new Exception($"Error while reading Reviews from database for book id { bookId }.", ex);
            }
        }

        public Review Get(long bookId, long reviewId)
        {
            try
            {
                _crudRepository.Get<Book>(bookId);

                var queryParameter = new { Id = reviewId };

                var query = @"SELECT *
                            FROM Review r 
                            INNER JOIN User u ON r.UserId = u.Id
                            Where r.Id = @Id;";

                var review = _databaseProvider
                    .GetConnection()
                    .Query<Review, User, Review>(query, IncludeUser, queryParameter)
                    .FirstOrDefault() ?? throw new EntityNotFoundException(nameof(Review), reviewId);

                return review;
            }
            catch (SqlException ex)
            {
                throw new Exception("Error while reading Review from database.", ex);
            }
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