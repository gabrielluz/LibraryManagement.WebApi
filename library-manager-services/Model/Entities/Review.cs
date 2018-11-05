using Dapper.Contrib.Extensions;

namespace LibraryManager.Models.Entities
{
    [Table("Review")]
    public class Review : IEntity
    {
        [Key]
        public long Id { get; set; }
        public string Comment { get; set; }
        public int Rate { get; set; }
        public User User { get; set; }
        public Book Book { get; set; }
    }
}