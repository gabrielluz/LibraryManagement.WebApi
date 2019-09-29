using System;

namespace LibraryManager.Api.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException(string name, long id)
            : base($"{ name } with id { id } not found.")
        {
        }
    }
}