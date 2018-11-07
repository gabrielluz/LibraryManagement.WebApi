using System;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManager.Filters
{
    internal class EntityNotFoundExceptionHandler : IExceptionHandler
    {
        public IActionResult HandleException(Exception ex)
        {
            throw new System.NotImplementedException();
        }
    }
}