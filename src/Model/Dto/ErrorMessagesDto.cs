using System.Collections.Generic;

namespace LibraryManagerApi.Models.Dto
{
    public class ErrorMessagesDto
    {
        public ErrorMessagesDto(IEnumerable<string> errors)
        {
            this.Errors = errors;
        }
        
        public IEnumerable<string> Errors { get; set; }        
    }
}