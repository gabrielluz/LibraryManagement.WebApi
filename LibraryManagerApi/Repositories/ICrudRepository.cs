using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper.Contrib.Extensions;
using LibraryManagerApi.Exceptions;
using LibraryManagerApi.Models.Entities;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace LibraryManagerApi.Repositories
{
    public interface ICrudRepository
    {
        IEnumerable<T> GetAll<T>() where T : class, IEntity;

        T Get<T>(long id) where T : class, IEntity;

        T Insert<T>(T entity) where T : class, IEntity;

        T Update<T>(long id, T entity) where T : class, IEntity;

        void Delete<T>(long id) where T : class, IEntity;
    }
}