using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper.Contrib.Extensions;
using LibraryManager.Api.Exceptions;
using LibraryManager.Api.Models.Entities;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace LibraryManager.Api.Repositories
{
    public class CrudRepository : ICrudRepository
    {
        private readonly IDbConnection _databaseConnection;

        public CrudRepository(IDatabaseProvider databaseProvider)
        {
            _databaseConnection = databaseProvider.GetConnection();
        }

        public IEnumerable<T> GetAll<T>() where T : class, IEntity
        {
            return _databaseConnection.GetAll<T>();
        }

        public T Get<T>(long id) where T : class, IEntity 
        {
            var entity = _databaseConnection.Get<T>(id);

            if (entity == null)
                throw new EntityNotFoundException(typeof(T).Name, id);
                
            return entity;
        }

        public T Insert<T>(T entity) where T : class, IEntity
        {
            entity.Id = _databaseConnection.Insert<T>(entity);
            return entity;
        }

        public T Update<T>(long id, T entity) where T : class, IEntity 
        {
            if (!_databaseConnection.Update<T>(entity))
                throw new EntityNotFoundException(typeof(T).Name, id);
            
            entity.Id = id;
            return entity;
        }

        public void Delete<T>(long id) where T : class, IEntity
        {
            _databaseConnection.Delete(Get<T>(id));
        }
    }
}