using System;

namespace LibraryManagerApi.Exceptions
{
    internal class ExceptionHandlerAbstractFactory
    {
        internal static ExceptionHandlerStrategy Build(Exception ex) 
        {
            switch (ex)
            {
                case EntityNotFoundException notFoundException:
                    return new EntityNotFoundExceptionHandler(notFoundException);
                case ArgumentException argumentException:
                    return new ArgumentExceptionHandler(argumentException);
            }
            return new GenericExceptionHandler(ex);
        }
    }
}