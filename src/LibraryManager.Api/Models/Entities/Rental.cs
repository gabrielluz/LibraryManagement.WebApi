using Dapper.Contrib.Extensions;
using System;

namespace LibraryManager.Api.Models.Entities
{
    [Table("Rental")]
    public class Rental : IEntity
    {
        [Key]
        public long Id { get; set; }
        public DateTime Issued { get; set; }
        public DateTime? Returned { get; set; }
        public User User { get; set; }
        public Book Book { get; set; }
    }
}