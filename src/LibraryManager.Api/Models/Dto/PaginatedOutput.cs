using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManager.Api.Models.Dto
{
    public class PaginatedOutput<T> where T : class
    {
        public PaginatedOutput(PaginationFilter paginationFilter, IEnumerable<T> items)
        {
            Limit = paginationFilter.Limit;
            Page = paginationFilter.Page;
            Items = items;
        }
        public int Limit { get; set; }
        public int Page { get; set; }
        public IEnumerable<T> Items { get; set; }
    }
}
