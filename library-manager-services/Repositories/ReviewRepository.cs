using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using Dapper.Contrib.Extensions;
using LibraryManager.Exceptions;
using LibraryManager.Models.Dto;
using LibraryManager.Models.Entities;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace LibraryManager.Repositories
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

            // var user = _crudRepository.Get<User>(review.User.Id);
            // var book = _crudRepository.Get<Book>(review.Book.Id);
            // var sqlCommand = "INSERT INTO Review(Comment, Rate, UserId, BookId) "
            //     + "VALUES (@Comment, @Rate, @UserId, @BookId);"
            //     + "SELECT LAST_INSERT_ID();";
            // var sqlParams = new 
            // {
            //     Comment = review.Comment,
            //     Rate = review.Rate,
            //     UserId = user.Id,
            //     BookId = book.Id
            // };
            var connection = _databaseProvider.GetConnection();
            long insertedId = connection.Insert<Review>(review);
            // using (var reader = connection.ExecuteReader(sqlCommand, sqlParams))
            // {
            //     if (reader.Read())
            //         insertedId = reader.GetInt32(0);
            // }
            return Get(insertedId);
        }

        public Review Update(long id, Review review) 
        {
            // var sql = "UPDATE Review SET Comment = @Comment, Rate = @Rate WHERE Id = @Id;";
            // var sqlParameters = new {
            //     Comment = review.Comment,
            //     Rate = review.Rate,
            //     Id = id
            // };
            // _crudRepository.Get<User>(review.User.Id);
            // _crudRepository.Get<Book>(review.Book.Id);
            // _databaseProvider.GetConnection().Execute(sql, sqlParameters);
            _databaseProvider.GetConnection().Update(review);
            return Get(review.Id);
        }

        public IEnumerable<Review> GetAll() 
        {
            try
            {
                string sql = @"SELECT * 
                            FROM Review r 
                            INNER JOIN User u ON r.UserId = u.Id
                            INNER JOIN Book b ON r.BookId = b.Id
                            ORDER BY r.Id;";
                
                var reviews = _databaseProvider.GetConnection()
                    .Query<Review, User, Book, Review>(sql, (review, user, book) => {
                        review.User = user;
                        review.Book = book;
                        return review;
                    });
                
                return reviews;
            }
            catch (SqlException ex)
            {
                throw new Exception("Error while reading Reviews from database.", ex);
            }
        }

        public Review Get(long id) 
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