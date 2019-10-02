using Microsoft.AspNetCore.Http;
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
            return response.WriteAsync(json);
        }
    }
}
