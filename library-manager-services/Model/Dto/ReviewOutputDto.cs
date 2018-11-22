using System;

namespace LibraryManager.Models.Dto
{
    public class ReviewOutputDto
    {
        public long Id { get; set; }
        public int Rate { get; set; }
        public string UserUrl { get; set; }
        public string BookUrl { get; set; }
        public DateTime Issued { get; set; }
        public DateTime Returned { get; set; }
        public string Comment { get; set; }
    }
}