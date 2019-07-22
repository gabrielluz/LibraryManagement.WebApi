using System.ComponentModel.DataAnnotations;

namespace LibraryManager.Api.Models.Dto
{
    public class UpdateReviewDto
    {
        [Range(0, 10, ErrorMessage = "Rate is required and must be a value between 1 and 10.")]
        public int Rate { get; set; }
        public string Comment { get; set; }
    }
}