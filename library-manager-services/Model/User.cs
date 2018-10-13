namespace LibraryManager.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string SecretKey { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Description { get; set; }
    }
}