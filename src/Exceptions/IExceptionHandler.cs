using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace LibraryManagerApi.Exceptions
{
    public interface IExceptionHandler
    {
        Task HandleException(HttpResponse response);
    }
}