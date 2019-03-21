using System.Collections.Generic;
using System.Linq;
using LibraryManagerApi.Exceptions;
using LibraryManagerApi.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace LibraryManagerApi.Filters
{
    public class ValidationActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ModelState.IsValid)
                return;

            // throw new InvalidInputException(BuildErrorMessageEnumerable(context));
            var errorMessagessDto = new ErrorMessagesDto(BuildErrorMessageEnumerable(context));
            context.Result = new BadRequestObjectResult(errorMessagessDto);
        }

        private IEnumerable<string> BuildErrorMessageEnumerable(ActionExecutingContext context)
        {
            return context.ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage);
        }
    }
}