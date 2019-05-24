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
    public interface IRentalRepository
    {
        Rental Insert(Rental review);
        Rental Update(Rental review);
        IEnumerable<Rental> GetAll();
        Rental Get(long id);
        void Delete(long id);
    }
}