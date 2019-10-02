using Microsoft.AspNetCore.Http;
using System.Net;
using System.Threading.Tasks;

namespace LibraryManager.Api.Exceptions.Handlers
{
    public class EntityAlreadyExistsExceptionHandler : BaseExceptionHandler
    {
        private readonly EntityAlreadyExistsException _exception;

        public EntityAlreadyExistsExceptionHandler(EntityAlreadyExistsException exception)
        {
            _exception = exception;
        }

        public override Task HandleException(HttpResponse response)
        {
            string json = SerializeErrorMessage(_exception.Message);
            response.StatusCode = (int)HttpStatusCode.BadRequest;
            return response.WriteAsync(json);
        }
    }
}
