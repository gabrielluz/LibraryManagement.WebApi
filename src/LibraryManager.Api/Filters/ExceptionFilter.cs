using LibraryManager.Api.Exceptions.Handlers;
using Microsoft.AspNetCore.Mvc.Filters;

namespace LibraryManager.Api.Filters
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
            context.HttpContext.Response.ContentType = "application/json";
            var exceptionHandler = _exceptionHandlerFactory.Build(context.Exception);
            exceptionHandler.HandleException(context.HttpContext.Response);
        }
    }
}