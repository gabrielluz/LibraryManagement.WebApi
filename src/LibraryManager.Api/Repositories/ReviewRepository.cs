using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using Dapper.Contrib.Extensions;
using LibraryManager.Api.Exceptions;
using LibraryManager.Api.Models.Dto;
using LibraryManager.Api.Models.Entities;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace LibraryManager.Api.Repositories
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
                Comment = review.Comment,
                Rate = review.Rate,
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
            var sql = "UPDATE Review SET Comment = @Comment, Rate = @Rate WHERE Id = @Id AND BookId = @BookId;";
            var sqlParameters = new {
                Comment = review.Comment,
                Rate = review.Rate,
                Id = review.Id,
                bookId = bookId
            };
            _databaseProvider.GetConnection().Execute(sql, sqlParameters);
            return Get(bookId, review.Id);
        }

        public IEnumerable<Review> GetAll(long bookId) 
        {
            try
            {
                string sql = @"SELECT * 
                            FROM Review r 
                            INNER JOIN User u ON r.UserId = u.Id
                            INNER JOIN Book b ON r.BookId = b.Id
                            WHERE BookId = @BookId
                            ORDER BY r.Id;";
                
                var reviews = _databaseProvider.GetConnection()
                    .Query<Review, User, Book, Review>(
                        sql, 
                        (review, user, book) => {
                            review.User = user;
                            review.Book = book;
                            return review;
                        },
                        new { BookId = bookId });
                
                return reviews;
            }
            catch (SqlException ex)
            {
                throw new Exception($"Error while reading Reviews from database for book id { bookId }.", ex);
            }
        }

        public Review Get(long bookId, long id) 
        {
            try 
            {
                var queryParameter = new { Id = id };
                
                var query = @"SELECT *
                            FROM Review r 
                            INNER JOIN User u ON r.UserId = u.Id
                            Where r.Id = @Id;";
                
                var review = _databaseProvider
                    .GetConnection()
                    .Query<Review, User, Review>(query, IncludeUser, queryParameter)
                    .FirstOrDefault() ?? throw new EntityNotFoundException(nameof(Review), id);

                return review;
            }
            catch (SqlException ex)
            {
                throw new Exception("Error while reading Review from database.", ex);
            }
        }

        public void Delete(long id) => _crudRepository.Delete<Review>(id);

        private Review IncludeUser(Review review, User user)
        {
            review.User = user;
            return review;
        }
    }
}