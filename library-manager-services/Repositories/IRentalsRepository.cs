using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using Dapper.Contrib.Extensions;
using LibraryManager.Exceptions;
using LibraryManager.Models.Dto;
using LibraryManager.Models.Entities;
using Microsoft.Extensions.Configuration;

namespace LibraryManager.Repositories
{
    public interface IRentalRepository
    {
        Rental Insert(Rental review);
        Rental Update(Rental review);
        IEnumerable<Rental> GetAll();
        Rental Get(long id);
        void Delete(long id);
    }
}