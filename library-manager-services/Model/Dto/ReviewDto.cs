using System;
using System.ComponentModel.DataAnnotations;

namespace LibraryManager.Models.Dto
{
    public class ReviewDto
    {
        public long Id { get; set; }
        
        [Range(1, int.MaxValue, ErrorMessage = "Rate is required.")]
        public int Rate { get; set; }
        
        [Range(1, long.MaxValue, ErrorMessage = "UserId is required.")]
        public long UserId { get; set; }
        
        [Range(1, long.MaxValue, ErrorMessage = "BookId is required.")]
        public long BookId { get; set; }
        
        [Required(ErrorMessage = "Issued date is required.")]
        public DateTime Issued { get; set; }
        public DateTime Returned { get; set; }
        public string Comment { get; set; }
    }
}