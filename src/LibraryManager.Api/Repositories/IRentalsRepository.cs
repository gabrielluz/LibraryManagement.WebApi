using LibraryManager.Api.Models;
using LibraryManager.Api.Models.Entities;
using System.Collections.Generic;

namespace LibraryManager.Api.Repositories
{
    public interface IRentalRepository
    {
        Rental Insert(Rental review);
        Rental Update(Rental review);
        IEnumerable<Rental> GetAllPaginated(PaginationFilter paginationFilter);
        IEnumerable<Rental> GetAll();
        Rental Get(long id);
        void Delete(long id);
    }
}