﻿using Dapper;
using LibraryManager.Api.Exceptions;
using LibraryManager.Api.Models;
using LibraryManager.Api.Models.Entities;
using LibraryManager.Api.Repositories.Interfaces;
using LibraryManager.Api.Repositories.Providers;
using LibraryManager.Api.Security;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public User Update(User user) => _crudRepository.Update(user);

        private bool EmailHasAlreadyBeenInserted(string email)
        {
            if (email == null)
                throw new ArgumentNullException(nameof(email));

            using (var connection = _databaseProvider.GetConnection())
            {
                string sql = "SELECT Id FROM User WHERE Email = @Email;";
                var parameters = new { Email = email };
                var userId = connection.QuerySingleOrDefault<long?>(sql, parameters);
                return userId.HasValue;
            }
        }

        public User Authenticate(Credentials credentials)
        {
            if (credentials == null)
                throw new ArgumentNullException(nameof(credentials));

            using (var connection = _databaseProvider.GetConnection())
            {
                string sql = "SELECT * FROM User WHERE Email = @Email AND SecretKey = @SecretKey";
                var parameters = new 
                {
                    Email = credentials.Email,
                    SecretKey = credentials.Password
                };
                var user = connection.QuerySingleOrDefault<User>(sql, parameters);
                return user;
            }
        }

        public User Insert(Credentials credentials)
        {
            if (credentials == null)
                throw new ArgumentNullException(nameof(credentials));

            if (EmailHasAlreadyBeenInserted(credentials.Email))
                throw new EntityAlreadyExistsException(nameof(User));

            int userId = 0;

            using (var connection = _databaseProvider.GetConnection())
            {
                string sql = "INSERT INTO User(Email, SecretKey) VALUES (@Email, @SecretKey); SELECT LAST_INSERT_ID();";
                var parameters = new
                {
                    Email = credentials.Email,
                    SecretKey = credentials.Password
                };
                
                var reader = connection.ExecuteReader(sql, parameters);
                if (reader.Read())
                    userId = reader.GetInt32(0);
            }
            return Get(userId);
        }
    }
}
