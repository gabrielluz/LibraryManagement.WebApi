using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace LibraryManagerApi.Exceptions
{
    public abstract class ExceptionHandlerStrategy : IExceptionHandler
    {
        public static Task HandleException(HttpResponse response, Exception ex) 
        {
            response.ContentType = "application/json";
            return ExceptionHandlerAbstractFactory.Build(ex).HandleException(response);
        }

        public string SerializeErrorMessage(string message) => JsonConvert.SerializeObject(new { message });

        public abstract Task HandleException(HttpResponse response);
    }
}