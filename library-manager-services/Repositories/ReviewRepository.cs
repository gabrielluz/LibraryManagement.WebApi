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
            var sql = "INSERT INTO Review(Comment, Rate, UserId, BookId) "
                    + "VALUES (@Comment, @Rate, @UserId, @BookId);";
            
            var user = _crudRepository.Get<User>(review.User.Id);
            var book = _crudRepository.Get<Book>(review.Book.Id);
            var connection = _databaseProvider.GetConnection();
            
            connection.Execute(sql, new {
                Comment = review.Comment,
                Rate = review.Rate,
                UserId = user.Id,
                BookId = book.Id
            });
            review.Book = book;
            review.User = user;
            return review;
        }

        public Review Update(int id, Review review) 
        {
            var connection = _databaseProvider.GetConnection();
            var user = _crudRepository.Get<User>(review.User?.Id);
            var book = _crudRepository.Get<Book>(review.Book?.Id);
            var sql = "UPDATE Review SET Comment = @Comment, Rate = @Rate "
                    + "WHERE Id = @Id;";

            connection.Execute(sql, new { 
                Comment = review.Comment,
                Rate = review.Rate,
                Id = id
            });
            review.User = user;
            review.Book = book;
            review.Id = id;
            return review;
        }

        public IEnumerable<Review> GetAll() 
        {
            try
            {
                var sql = @"SELECT 
                            r.Id, r.Rate, r.Comment, b.*, u.* 
                            FROM Review r 
                            INNER JOIN Book b ON r.BookId = b.Id
                            INNER JOIN User u ON r.UserId = u.Id;";
                var reader = _databaseProvider.GetConnection().ExecuteReader(sql);
                var reviews = new Collection<Review>();
                while (reader.Read())
                    reviews.Add(ReadReviewFromDatabase(reader));

                return reviews;
            }
            catch (SqlException ex)
            {
                throw new Exception("Error while reading Reviews from database.", ex);
            }
        }

        public Review Get(int id) 
        {
            try 
            {
                var sql = @"SELECT 
                            r.Id, r.Rate, r.Comment, b.*, u.* 
                            FROM Review r 
                            INNER JOIN Book b ON r.BookId = b.Id
                            INNER JOIN User u ON r.UserId = u.Id
                            Where r.Id = @Id;";
                var reader = _databaseProvider.GetConnection().ExecuteReader(sql, new { Id = id });
                if (!reader.Read())
                    throw new EntityNotFoundException<Review>(id);

                var review = ReadReviewFromDatabase(reader);
                return review;
            }
            catch (SqlException ex)
            {
                throw new Exception("Error while reading Review from database.", ex);
            }
        } 

        public void Delete(int id) => _crudRepository.Delete<Review>(id);

        private Review ReadReviewFromDatabase(IDataReader reader)
        {
            var user = ReadUserFromDatabase(reader);
            var book = ReadBookFromDatabase(reader);
            var review = new Review
            {
                Id = reader.GetInt64(0),
                Rate = reader.GetInt16(1),
                Comment = reader.GetString(2),
                User = ReadUserFromDatabase(reader),
                Book = ReadBookFromDatabase(reader)
            };
            return review;
        }

        private Book ReadBookFromDatabase(IDataReader reader)
        {
            var book = new Book
            {
                Id = reader.GetInt32(3),
                Title = reader.GetString(4),
                Author = reader.GetString(5),
                Description = reader.GetString(6)
            };
            return book;
        }

        private User ReadUserFromDatabase(IDataReader reader)
        {
            var user = new User
            {
                Id = reader.GetInt32(7),
                Email = reader.GetString(8),
                FirstName = reader.GetString(10),
                LastName = reader.GetString(11),
                Description = reader.GetString(12)
            };
            return user;
        }
    }
}