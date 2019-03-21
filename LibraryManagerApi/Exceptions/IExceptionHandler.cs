using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace LibraryManagerApi.Exceptions
{
    internal interface IExceptionHandler
    {
        Task HandleException(HttpResponse response);
    }
}