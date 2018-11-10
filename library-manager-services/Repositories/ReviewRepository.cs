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

        public Review Insert(ReviewDto reviewDto)
        {
            if (reviewDto == null)
                throw new ArgumentNullException("Review must be not null.");

            var user = _crudRepository.Get<User>(reviewDto.UserId);
            var book = _crudRepository.Get<Book>(reviewDto.BookId);
            var sqlCommand = "INSERT INTO Review(Comment, Rate, UserId, BookId) "
                + "VALUES (@Comment, @Rate, @UserId, @BookId);"
                + "SELECT LAST_INSERT_ID();";
            var sqlParams = new 
            {
                Comment = reviewDto.Comment,
                Rate = reviewDto.Rate,
                UserId = user.Id,
                BookId = book.Id
            };
            var connection = _databaseProvider.GetConnection();
            int insertedId = 0;
            using (var reader = connection.ExecuteReader(sqlCommand, sqlParams))
            {
                if (reader.Read())
                    insertedId = reader.GetInt32(0);
            }
            return Get(insertedId);
        }

        public Review Update(long id, ReviewDto reviewDto) 
        {
            User user = _crudRepository.Get<User>(reviewDto.UserId);
            Book book = _crudRepository.Get<Book>(reviewDto.BookId);
            string sql = "UPDATE Review SET Comment = @Comment, Rate = @Rate WHERE Id = @Id;";
            int amountOfRecordsAffected = _databaseProvider.GetConnection().Execute(sql, new {
                Comment = reviewDto.Comment,
                Rate = reviewDto.Rate,
                Id = id
            });
            return Get(reviewDto.Id);
        }

        public IEnumerable<Review> GetAll() 
        {
            try
            {
                string sql = @"SELECT 
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

        public Review Get(long id) 
        {
            try 
            {
                string sql = @"SELECT 
                            r.Id, r.Rate, r.Comment, b.*, u.* 
                            FROM Review r 
                            INNER JOIN Book b ON r.BookId = b.Id
                            INNER JOIN User u ON r.UserId = u.Id
                            Where r.Id = @Id;";
                var reader = _databaseProvider.GetConnection().ExecuteReader(sql, new { Id = id });
                
                if (!reader.Read())
                    throw new EntityNotFoundException<Review>(nameof(Review), id);

                return ReadReviewFromDatabase(reader);
            }
            catch (SqlException ex)
            {
                throw new Exception("Error while reading Review from database.", ex);
            }
        } 

        public void Delete(long id) => _crudRepository.Delete<Review>(id);

        private Review ReadReviewFromDatabase(IDataReader reader)
        {
            User user = ReadUserFromDatabase(reader);
            Book book = ReadBookFromDatabase(reader);
            return new Review
            {
                Id = reader.GetInt64(0),
                Rate = reader.GetInt16(1),
                Comment = reader.GetString(2),
                User = ReadUserFromDatabase(reader),
                Book = ReadBookFromDatabase(reader)
            };
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