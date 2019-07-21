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

        public T Insert<T>(T newEntity) where T : class, IEntity
        {
            if (newEntity == null)
                throw new ArgumentNullException(nameof(newEntity));

            newEntity.Id = _databaseConnection.Insert<T>(newEntity);
            return newEntity;
        }

        public T Update<T>(T entity) where T : class, IEntity 
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _databaseConnection.Update<T>(entity);
            return Get<T>(entity.Id);
        }

        public void Delete<T>(long id) where T : class, IEntity
        {
            Get<T>(id);
            _databaseConnection.Delete(Get<T>(id));
        }
    }
}