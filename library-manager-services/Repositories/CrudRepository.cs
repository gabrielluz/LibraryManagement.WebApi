using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper.Contrib.Extensions;
using LibraryManager.Exceptions;
using LibraryManager.Models.Entities;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace LibraryManager.Repositories
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

        public T Get<T>(long? id) where T : class, IEntity 
        {
            if (!id.HasValue)
                throw new ArgumentNullException("id");

            var entity = _databaseConnection.Get<T>(id);

            if (entity == null)
                throw new EntityNotFoundException<T>(id.Value);
                
            return entity;
        }

        public T Insert<T>(T entity) where T : class, IEntity
        {
            entity.Id = _databaseConnection.Insert<T>(entity);
            return entity;
        }

        public T Update<T>(long? id, T entity) where T : class, IEntity 
        {
            if (!id.HasValue)
                throw new ArgumentNullException("id");

            if (!_databaseConnection.Update<T>(entity))
                throw new EntityNotFoundException<T>(id.Value);
            
            entity.Id = id.Value;
            return entity;
        }

        public void Delete<T>(long? id) where T : class, IEntity
        {
            if (!id.HasValue)
                throw new ArgumentNullException("id");

            var entity = _databaseConnection.Get<T>(id);

            if (entity == null)
                throw new EntityNotFoundException<T>(id.Value);
            
            _databaseConnection.Delete(entity);
        }
    }
}