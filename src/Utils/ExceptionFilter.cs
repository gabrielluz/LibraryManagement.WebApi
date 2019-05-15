using LibraryManagerApi.Exceptions;
using Microsoft.AspNetCore.Mvc.Filters;

namespace src.Utils
{
    public class ExceptionFilter : ExceptionFilterAttribute
    {
        private ExceptionHandlerAbstractFactory _exceptionHandlerFactory;

        public ExceptionFilter(ExceptionHandlerAbstractFactory exceptionHandlerFactory)
        {
            _exceptionHandlerFactory = exceptionHandlerFactory;
        }

        public override void OnException(ExceptionContext context)
        {
            var exceptionHandler = _exceptionHandlerFactory.Build(context.Exception);
            exceptionHandler.HandleException(context.HttpContext.Response);
        }
    }
}