using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using Dapper.Contrib.Extensions;
using LibraryManager.Api.Exceptions;
using LibraryManager.Api.Models.Dto;
using LibraryManager.Api.Models.Entities;
using Microsoft.Extensions.Configuration;

namespace LibraryManager.Api.Repositories
{
    public interface IReviewRepository
    {
        Review Insert(Review review);
        Review Update(long bookId, Review review);
        IEnumerable<Review> GetAll(long bookId);
        Review Get(long bookId, long reviewId);
        void Delete(long id);
    }
}