using System;

namespace LibraryManager.Api.Models.Dto
{
    public class RentalOutputDto
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public long BookId { get; set; }
        public DateTime Issued { get; set; }
        public DateTime? Returned { get; set; }
    }
}