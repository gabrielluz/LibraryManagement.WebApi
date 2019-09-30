using LibraryManager.Api.Models;
using LibraryManager.Api.Models.Entities;
using System.Collections.Generic;

namespace LibraryManager.Api.Repositories.Interfaces
{
    public interface IUsersRepository
    {
        IEnumerable<User> GetAllPaginated(Pagination paginationFilter);

        User Get(long id);

        User Insert(User entity);

        User Update(User entity);

        void Delete(long id);
    }
}
