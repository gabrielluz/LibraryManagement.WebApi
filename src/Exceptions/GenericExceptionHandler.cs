using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace LibraryManagerApi.Exceptions
{
    internal class GenericExceptionHandler : BaseExceptionHandler
    {
        private readonly Exception _ex;

        public GenericExceptionHandler(Exception ex)
        {
            _ex = ex;
        }

        public override Task HandleException(HttpResponse response)
        {
            response.StatusCode = StatusCodes.Status500InternalServerError;
            return response.WriteAsync(SerializeErrorMessage("Internal server error."));
        }
    }
}