using LibraryManager.Api.Models;
using LibraryManager.Api.Models.Entities;
using System.Collections.Generic;

namespace LibraryManager.Api.Repositories.Interfaces
{
    public interface IBooksRepository
    {
        IEnumerable<Book> GetAllPaginated(Pagination paginationFilter);

        Book Get(long id);

        Book Insert(Book entity);

        Book Update(Book entity);

        void Delete(long id);
    }
}
