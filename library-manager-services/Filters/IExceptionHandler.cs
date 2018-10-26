using System;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManager.Filters
{
    public interface IExceptionHandler 
    {
        IActionResult HandleException(Exception ex);
    }
}