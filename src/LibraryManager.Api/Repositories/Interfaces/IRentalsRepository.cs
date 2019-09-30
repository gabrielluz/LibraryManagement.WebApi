using LibraryManager.Api.Models;
using LibraryManager.Api.Models.Entities;
using System.Collections.Generic;

namespace LibraryManager.Api.Repositories.Interfaces
{
    public interface IRentalRepository
    {
        Rental Insert(Rental review);
        Rental Update(Rental review);
        IEnumerable<Rental> GetAllPaginated(Pagination paginationFilter);
        IEnumerable<Rental> GetAll();
        Rental Get(long id);
        void Delete(long id);
    }
}