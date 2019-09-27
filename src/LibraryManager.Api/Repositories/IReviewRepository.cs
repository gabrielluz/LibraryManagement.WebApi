using LibraryManager.Api.Models.Entities;
using System.Collections.Generic;

namespace LibraryManager.Api.Repositories
{
    public interface IReviewRepository
    {
        Review Insert(Review review);
        Review Update(long bookId, Review review);
        IEnumerable<Review> GetAll(long bookId);
        Review Get(long bookId, long reviewId);
        void Delete(long bookId, long reviewId);
    }
}