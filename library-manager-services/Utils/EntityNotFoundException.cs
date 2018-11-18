using System;
using LibraryManager.Models.Entities;

namespace LibraryManager.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException(string name, long id) 
            : base($"{ name } with id { id } not found.")
        {
        }
    }
}