using System.ComponentModel.DataAnnotations;

namespace LibraryManager.Api.Models
{
    public class PaginationFilter
    {
        public PaginationFilter()
        {
            Page = 1;
            Limit = 20;
        }

        [Range(1, int.MaxValue)]
        public int Page { get; set; }

        [Range(1, 40)]
        public int Limit { get; set; }

        public int CalculateOffSet()
        {
            return (Page - 1) * Limit;
        }
    }
}
