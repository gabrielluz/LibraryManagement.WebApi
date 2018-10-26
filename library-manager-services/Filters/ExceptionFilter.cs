using LibraryManager.Exceptions;
using LibraryManager.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;

namespace LibraryManager.Filters
{
    public class ExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {

            if (context.Exception is EntityNotFoundException<IEntity>)
            {
                
            }
        }
    }
}