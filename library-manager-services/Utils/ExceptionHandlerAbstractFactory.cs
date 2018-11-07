using System;
using LibraryManager.Exceptions;
using LibraryManager.Models.Entities;

namespace LibraryManager.Filters
{
    public class ExceptionHandlerAbstractFactory
    {
        public static IExceptionHandler Make(Exception ex) 
        {
            if (ex is EntityNotFoundException<IEntity>)
                return new EntityNotFoundExceptionHandler();

            if (ex is ArgumentException)
                return null;

            return new DefaultExceptionHandler();
        }
    }
}