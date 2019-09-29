using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace LibraryManager.Api.Exceptions.Handlers
{
    public interface IExceptionHandler
    {
        Task HandleException(HttpResponse response);
    }
}