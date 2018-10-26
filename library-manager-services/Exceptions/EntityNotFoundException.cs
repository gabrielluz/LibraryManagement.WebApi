using System;
using LibraryManager.Models;

namespace LibraryManager.Exceptions
{
    public class EntityNotFoundException<T> : Exception where T : class, IEntity
    {
        public EntityNotFoundException(T entity, long id) 
            : base($"{ typeof(T).Name } with id { id } not found.")
        {
        }
    }
}