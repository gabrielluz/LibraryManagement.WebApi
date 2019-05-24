using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using Dapper.Contrib.Extensions;
using LibraryManagerApi.Exceptions;
using LibraryManagerApi.Models.Dto;
using LibraryManagerApi.Models.Entities;
using Microsoft.Extensions.Configuration;

namespace LibraryManagerApi.Repositories
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