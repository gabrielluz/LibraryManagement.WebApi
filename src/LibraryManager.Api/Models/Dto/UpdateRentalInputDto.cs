using System;
using System.ComponentModel.DataAnnotations;

namespace LibraryManager.Api.Models.Dto
{
    public class UpdateRentalInputDto
    {
        public DateTime Returned { get; set; }
    }
}