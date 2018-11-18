using System.Collections.Generic;
using LibraryManager.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace LibraryManager.Filters
{
    public class ValidationActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ModelState.IsValid)
                return;

            var errors = CreateErrorListFromModelState(context);
            context.Result = new BadRequestObjectResult(errors);
        }

        private ErrorMessagesDto CreateErrorListFromModelState(ActionExecutingContext context)
        {
            var errorList = new List<string>();
            foreach (var key in context.ModelState.Keys)
            {
                foreach (var error in context.ModelState[key].Errors)
                    errorList.Add(error.ErrorMessage);
            }
            return new ErrorMessagesDto(errorList);
        }
    }
}