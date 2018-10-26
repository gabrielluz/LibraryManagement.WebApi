using Dapper.Contrib.Extensions;

namespace LibraryManager.Models
{
    [Table("Book")]
    public class Book : IEntity
    {
        [Key]
        public long Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
    }
}