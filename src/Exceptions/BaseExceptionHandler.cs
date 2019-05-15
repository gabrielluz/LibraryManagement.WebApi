using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace LibraryManagerApi.Exceptions
{
    public abstract class BaseExceptionHandler : IExceptionHandler
    {
        protected string SerializeErrorMessage(string message) => JsonConvert.SerializeObject(new { message });

        public abstract Task HandleException(HttpResponse response);
    }
}