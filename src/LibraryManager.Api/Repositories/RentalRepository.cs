using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using Dapper.Contrib.Extensions;
using LibraryManager.Api.Exceptions;
using LibraryManager.Api.Models.Dto;
using LibraryManager.Api.Models.Entities;
using Microsoft.Extensions.Configuration;

namespace LibraryManager.Api.Repositories
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
            string sql = @"INSTERT INTO Rental(UserId, BookId, Issued
                           VALUES(@UserId, @BookId, @Issued);
                           SELECT LAST_INSERT_ID();";
            
            var reader = _databaseProvider
                .GetConnection()
                .ExecuteReader(sql, new {
                    UserId = rental.User.Id,
                    BookId = rental.Book.Id,
                    Issued = DateTime.Now
                });
            reader.Read();
            return Get(reader.GetInt32(0));
        }

        public Rental Update(Rental rental) 
        {
            var sql = "UPDATE Rental SET Returned = @Returned WHERE Id = @Id;";
            var conn = _databaseProvider.GetConnection();
            conn.Execute(sql, new { rental.Returned, rental.Id });
            return Get(rental.Id);
        }

        public IEnumerable<Rental> GetAll() 
        {
            return _crudRepository.GetAll<Rental>();
        }

        public Rental Get(long id) 
        {
            return _crudRepository.Get<Rental>(id)
                ?? throw new EntityNotFoundException(nameof(Rental), id);
        }

        public void Delete(long id) 
        {
            Get(id);
            _crudRepository.Delete<Rental>(id);
        }
    }
}