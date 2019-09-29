using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace LibraryManager.Api.Exceptions
{
    public interface IExceptionHandler
    {
        Task HandleException(HttpResponse response);
    }
}