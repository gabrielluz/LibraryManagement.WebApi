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

namespace LibraryManager.Utils
{
    public static class DatabaseUtils
    {
        // public static string GetCommandWithOffSet(string selectCommand)
        // {
        //     // return selectCommand.Append(" OFFSET ");
        // }
    }
}