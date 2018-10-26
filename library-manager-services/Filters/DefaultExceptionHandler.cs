using System;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManager.Filters
{
    internal class DefaultExceptionHandler : IExceptionHandler
    {
        public IActionResult HandleException(Exception ex)
        {
            return null;
        }
    }
}