using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace LibraryManagerApi.Exceptions
{
    public class InvalidInputExceptionHandler : ExceptionHandlerStrategy
    {
        private readonly InvalidInputException _invalidInputException;

        public InvalidInputExceptionHandler(InvalidInputException invalidInputException)
        {
            _invalidInputException = invalidInputException;
        }

        public override Task HandleException(HttpResponse response)
        {
            response.StatusCode = StatusCodes.Status400BadRequest;
            return response.WriteAsync(GetSerializedErrorList());
        }

        private string GetSerializedErrorList()
        {
            return JsonConvert.SerializeObject(_invalidInputException.Errors);
        }
    }
}