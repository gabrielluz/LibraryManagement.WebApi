using Dapper.Contrib.Extensions;
using LibraryManager.Api.Exceptions;
using LibraryManager.Api.Models.Entities;
using LibraryManager.Api.Repositories.Interfaces;
using LibraryManager.Api.Repositories.Providers;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;

namespace LibraryManager.Api.Repositories.Implementations
{
    public class CrudRepository : ICrudRepository
    {
        private readonly IDatabaseProvider _databaseProvider;

        public CrudRepository(IDatabaseProvider databaseProvider)
        {
            _databaseProvider = databaseProvider;
        }

        public IEnumerable<T> GetAll<T>() where T : class, IEntity
        {
            using (var connection = _databaseProvider.GetConnection())
                return connection.GetAll<T>();
        }

        public T Get<T>(long id) where T : class, IEntity
        {
            using (var connection = _databaseProvider.GetConnection())
                return Get<T>(id, connection);
        }

        public T Insert<T>(T newEntity) where T : class, IEntity
        {
            if (newEntity == null)
                throw new ArgumentNullException(nameof(newEntity));

            using (var connection = _databaseProvider.GetConnection())
            {
                newEntity.Id = connection.Insert<T>(newEntity);
                return newEntity;
            }
        }

        public T Update<T>(T entity) where T : class, IEntity
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            using (var connection = _databaseProvider.GetConnection())
            {
                connection.Update(entity);
                return Get<T>(entity.Id, connection);
            }
        }

        public void Delete<T>(long id) where T : class, IEntity
        {
            using (var connection = _databaseProvider.GetConnection())
            {
                Get<T>(id, connection);
                connection.Delete(Get<T>(id));
            }
        }

        private static T Get<T>(long id, IDbConnection connection) where T : class, IEntity
        {
            var entity = connection.Get<T>(id);

            if (entity == null)
                throw new EntityNotFoundException(typeof(T).Name, id);

            return entity;
        }
    }
}