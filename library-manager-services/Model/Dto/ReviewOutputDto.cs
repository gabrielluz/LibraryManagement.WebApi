using System;
using LibraryManager.Models.Entities;

namespace LibraryManager.Models.Dto
{
    public class ReviewOutputDto
    {
        public long Id { get; set; }
        public int Rate { get; set; }
        public string Comment { get; set; }
        public User User { get; set; }
    }
}