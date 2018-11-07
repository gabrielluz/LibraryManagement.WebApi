using System;
using System.Collections.Generic;
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

        public Rental Insert(RentalDto rental) 
        {
            string sql = @"INSTERT INTO Review(UserId, BookId, Rate, Comment)
                           VALUES(@UserId, @BookId, @Rate, @Comment);
                           SELECT LAST_INSERT_ID();";
            
            int rentalId = _databaseProvider.GetConnection().Execute(sql, new {
                rental.UserId,
                rental.BookId,
                DateTime.Now
            });

            return Get(rentalId);
        }

        public Rental Update(long id, RentalDto review) 
        {
            return null;

        }

        public IEnumerable<Rental> GetAll() 
        {
            return null;

        }

        public Rental Get(long id) 
        {
            return null;

        }

        public void Delete(long id) 
        {
            _crudRepository.Delete<Rental>(id);
        }
    }
}