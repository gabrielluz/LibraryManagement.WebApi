using System.Collections.Generic;
using System.Linq;
using LibraryManager.Api.Exceptions;
using LibraryManager.Api.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace LibraryManager.Api.Filters
{
    public class ValidationActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ModelState.IsValid)
                return;

            throw new InvalidInputException(BuildErrorMessageEnumerable(context));
        }

        private IEnumerable<string> BuildErrorMessageEnumerable(ActionExecutingContext context)
        {
            return context.ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage);
        }
    }
}