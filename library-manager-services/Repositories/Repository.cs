using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Dapper.Contrib.Extensions;
using LibraryManager.Exceptions;
using LibraryManager.Models;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace LibraryManager.Repositories
{
    public class Repository<T> where T : class, IEntity
    {
        public IEnumerable<T> GetAll() 
        {
            using (var connection = DatabaseSingleton.GetConnection())
                return connection.GetAll<T>();
        }

        public T Get(int id) 
        {
            using (var connection = DatabaseSingleton.GetConnection())
            {
                var entity = connection.Get<T>(id);
                if (entity == null)
                    throw new EntityNotFoundException<T>(entity as T, id);
                return entity;
            }
        }

        public T Insert(T entity)
        {
            using (var connection = DatabaseSingleton.GetConnection())
            {
                entity.Id = connection.Insert<T>(entity);
                return entity;
            }
        }

        public T Update(int id, T entity) 
        {
            using (var connection = DatabaseSingleton.GetConnection())
            {
                if (!connection.Update<T>(entity))
                    throw new EntityNotFoundException<T>(entity as T, id);
            }
            
            entity.Id = id;
            return entity;
        }

        public void Delete(int id)
        {
            using (var conexao = DatabaseSingleton.GetConnection())
            {
                var entity = conexao.Get<T>(id);
                if (entity == null)
                    throw new EntityNotFoundException<T>(entity as T, id);
                conexao.Delete(entity);
            }
        }
    }
}