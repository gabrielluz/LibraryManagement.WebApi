using System;

namespace LibraryManager.Api.Exceptions
{
    public class ExceptionHandlerAbstractFactory
    {
        public IExceptionHandler Build(Exception exception)
        {
            switch (exception)
            {
                case EntityNotFoundException notFoundException:
                    return new EntityNotFoundExceptionHandler(notFoundException);

                case ArgumentException argumentException:
                    return new ArgumentExceptionHandler(argumentException);

                case InvalidInputException invalidInputException:
                    return new InvalidInputExceptionHandler(invalidInputException);
            }
            return new GenericExceptionHandler(exception);
        }
    }
}