using System.Collections.Generic;

namespace LibraryManager.Api.Models.Dto
{
    public class PaginatedOutput<T> where T : class
    {
        public PaginatedOutput(PaginationFilter paginationFilter, IEnumerable<T> items)
        {
            Limit = paginationFilter.Limit;
            Page = paginationFilter.Page;
            Items = items ?? new T[] { };
        }

        public int Limit { get; set; }
        public int Page { get; set; }
        public IEnumerable<T> Items { get; set; }
    }
}
