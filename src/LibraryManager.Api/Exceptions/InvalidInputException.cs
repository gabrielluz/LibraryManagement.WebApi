using System;
using System.Collections.Generic;

namespace LibraryManager.Api.Exceptions
{
    public class InvalidInputException : Exception
    {
        public InvalidInputException(IEnumerable<string> errors) : base("Model state is invalid")
        {
            Errors = errors;
        }
        
        public IEnumerable<string> Errors { get; }    
    }
}