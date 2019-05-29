using System;
using LibraryManager.Api.Models.Entities;

namespace LibraryManager.Api.Models.Dto
{
    public class ReviewOutputDto
    {
        public long Id { get; set; }
        public int Rate { get; set; }
        public string Comment { get; set; }
        public User User { get; set; }
    }
}