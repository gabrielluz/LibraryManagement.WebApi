using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace LibraryManager.Api.Exceptions.Handlers
{
    internal class ArgumentExceptionHandler : BaseExceptionHandler
    {
        private readonly ArgumentException _argumentException;

        public ArgumentExceptionHandler(ArgumentException argumentException)
        {
            _argumentException = argumentException;
        }

        public override Task HandleException(HttpResponse response)
        {
            response.StatusCode = StatusCodes.Status400BadRequest;
            return response.WriteAsync(SerializeErrorMessage(_argumentException.Message));
        }
    }
}