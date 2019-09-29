using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace LibraryManager.Api.Exceptions
{
    public abstract class BaseExceptionHandler : IExceptionHandler
    {
        protected string SerializeErrorMessage(string message)
        {
            string json = JsonConvert.SerializeObject(new { message });
            return json;
        }

        public abstract Task HandleException(HttpResponse response);
    }
}