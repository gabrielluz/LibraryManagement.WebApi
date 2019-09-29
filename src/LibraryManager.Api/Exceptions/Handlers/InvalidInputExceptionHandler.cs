using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace LibraryManager.Api.Exceptions.Handlers
{
    public class InvalidInputExceptionHandler : BaseExceptionHandler
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
            return JsonConvert.SerializeObject(new { errorMessages = _invalidInputException.Errors });
        }
    }
}