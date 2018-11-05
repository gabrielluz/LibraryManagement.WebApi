using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using Dapper.Contrib.Extensions;
using LibraryManager.Exceptions;
using LibraryManager.Models.Entities;
using Microsoft.Extensions.Configuration;

namespace LibraryManager.Repositories
{
    public interface IReviewRepository
    {
        Review Insert(Review review);
        Review Update(int id, Review review);
        IEnumerable<Review> GetAll();
        Review Get(int id);
        void Delete(int id);
    }
}