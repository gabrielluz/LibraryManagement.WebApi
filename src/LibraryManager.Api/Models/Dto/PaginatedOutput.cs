using System.Collections.Generic;

namespace LibraryManager.Api.Models.Dto
{
    public class PaginatedOutput<T> where T : class
    {
        public PaginatedOutput(Pagination paginationFilter, IEnumerable<T> items)
        {
            if (paginationFilter == null)
                paginationFilter = new Pagination();

            Limit = paginationFilter?.Limit ?? 20;
            Page = paginationFilter?.Page ?? 0;
            Items = items ?? new T[] { };
        }

        public int Limit { get; set; }
        public int Page { get; set; }
        public IEnumerable<T> Items { get; set; }
    }
}
