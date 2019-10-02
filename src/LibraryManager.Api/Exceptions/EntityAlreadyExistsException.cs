using System;

namespace LibraryManager.Api.Exceptions
{
    public class EntityAlreadyExistsException : Exception
    {
        public EntityAlreadyExistsException(string entityName)
            : base($"The {entityName} is already present in the database.")
        {
        }
    }
}
