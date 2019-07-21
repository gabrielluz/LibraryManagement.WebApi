using System.ComponentModel.DataAnnotations;

namespace LibraryManager.Api.Models.Dto
{
    public class UserInputDto
    {
        [Required]        
        public string Email { get; set; }
        
        [Required]
        public string FirstName { get; set; }
        
        public string LastName { get; set; }

        public string Description { get; set; }
    }
}