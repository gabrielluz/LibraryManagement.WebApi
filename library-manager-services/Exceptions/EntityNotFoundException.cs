using System;
using LibraryManager.Models.Entities;

namespace LibraryManager.Exceptions
{
    public class EntityNotFoundException<T> : Exception where T : class, IEntity
    {
        public EntityNotFoundException(long id) 
            : base($"{ typeof(T).Name } with id { id } not found.")
        {
        }
    }
}