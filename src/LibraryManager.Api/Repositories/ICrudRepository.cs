using LibraryManager.Api.Models.Entities;
using System.Collections.Generic;

namespace LibraryManager.Api.Repositories
{
    public interface ICrudRepository
    {
        IEnumerable<T> GetAll<T>() where T : class, IEntity;

        T Get<T>(long id) where T : class, IEntity;

        T Insert<T>(T entity) where T : class, IEntity;

        T Update<T>(T entity) where T : class, IEntity;

        void Delete<T>(long id) where T : class, IEntity;
    }
}