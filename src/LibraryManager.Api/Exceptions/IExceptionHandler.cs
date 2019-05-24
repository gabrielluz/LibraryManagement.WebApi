using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace LibraryManager.Api.Exceptions
{
    public interface IExceptionHandler
    {
        Task HandleException(HttpResponse response);
    }
}