using System;

namespace LibraryManager.Api.Exceptions
{
    public class EntityAlreadyExistsException : Exception
    {
        public EntityAlreadyExistsException(string entityName)
            : base($"The specified {entityName} already exists.")
        {
        }
    }
}
