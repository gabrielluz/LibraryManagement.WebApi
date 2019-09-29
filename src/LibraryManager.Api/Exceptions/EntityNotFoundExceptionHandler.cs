using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace LibraryManager.Api.Exceptions
{
    internal class EntityNotFoundExceptionHandler : BaseExceptionHandler
    {
        private readonly EntityNotFoundException _notFoundException;

        public EntityNotFoundExceptionHandler(EntityNotFoundException notFoundException)
        {
            _notFoundException = notFoundException;
        }

        public override Task HandleException(HttpResponse response)
        {
            response.StatusCode = StatusCodes.Status404NotFound;
            return response.WriteAsync(SerializeErrorMessage(_notFoundException.Message));
        }
    }
}