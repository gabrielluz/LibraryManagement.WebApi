using LibraryManager.Api.Models;
using LibraryManager.Api.Models.Entities;
using LibraryManager.Api.Security;
using System.Collections.Generic;

namespace LibraryManager.Api.Repositories.Interfaces
{
    public interface IUsersRepository
    {
        IEnumerable<User> GetAllPaginated(Pagination paginationFilter);

        User Get(long id);

        User Authenticate(Credentials credentials);

        User Insert(Credentials credentials);

        User Update(User entity);

        void Delete(long id);
    }
}
