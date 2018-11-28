using System;
using System.ComponentModel.DataAnnotations;

namespace LibraryManager.Models.Dto
{
    public class ReviewInputDto
    {
        
        [Range(1, 10, ErrorMessage = "Rate is required and must be a value between 1 and 10.")]
        public int Rate { get; set; }
        
        [Range(1, long.MaxValue, ErrorMessage = "UserId is required.")]
        public long UserId { get; set; }
        
        [Required(ErrorMessage = "Issued date is required.")]
        public string Comment { get; set; }
    }
}