using System;
using System.ComponentModel.DataAnnotations;

namespace LibraryManager.Api.Models.Dto
{
    public class AddRentalInputDto
    {
        [Required(ErrorMessage = "UserId is required.")]
        public long UserId { get; set; }
        
        [Required(ErrorMessage = "BookId is required.")]
        public long BookId { get; set; }
        
        [Required(ErrorMessage = "Issued date is required.")]
        public DateTime Issued { get; set; }
    }
}