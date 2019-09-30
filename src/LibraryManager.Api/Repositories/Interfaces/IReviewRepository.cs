using LibraryManager.Api.Models;
using LibraryManager.Api.Models.Entities;
using System.Collections.Generic;

namespace LibraryManager.Api.Repositories.Interfaces
{
    public interface IReviewRepository
    {
        Review Insert(Review review);
        Review Update(long bookId, Review review);
        IEnumerable<Review> GetAllPaginated(long bookId, Pagination paginationFilter);
        Review Get(long bookId, long reviewId);
        void Delete(long bookId, long reviewId);
    }
}