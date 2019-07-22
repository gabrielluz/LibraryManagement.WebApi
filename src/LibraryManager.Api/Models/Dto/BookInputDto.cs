using System.ComponentModel.DataAnnotations;

namespace LibraryManager.Api.Models.Dto
{
    public class BookInputDto
    {
        [Required(ErrorMessage = "Title is required")]
        [MaxLength(500, ErrorMessage = "Title can't have more than 500 chars.")]
        public string Title { get; set; }
        
        [Required]
        [MaxLength(500, ErrorMessage = "Author can't have more than 500 chars.")]
        public string Author { get; set; }
        
        [MaxLength(500, ErrorMessage = "Description can't have more than 500 chars.")]
        public string Description { get; set; }
    }
}