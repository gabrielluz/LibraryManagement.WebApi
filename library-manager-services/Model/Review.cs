namespace LibraryManager.Models
{
    public class Review
    {
        public int Id { get; set; }
        public string Comment { get; set; }
        public int Rate { get; set; }
        public User User { get; set; }
        public Book Book { get; set; } 
    }
}