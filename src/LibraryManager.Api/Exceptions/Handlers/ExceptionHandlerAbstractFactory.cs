using System;

namespace LibraryManager.Api.Exceptions.Handlers
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

                case EntityAlreadyExistsException entityAlreadyExistsException:
                    return new EntityAlreadyExistsExceptionHandler(entityAlreadyExistsException);
            }
            return new GenericExceptionHandler(exception);
        }
    }
}