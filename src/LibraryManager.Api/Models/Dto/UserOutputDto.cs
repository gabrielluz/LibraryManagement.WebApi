namespace LibraryManager.Api.Models.Dto
{
    public class UserOutputDto
    {
        public long Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string Description { get; set; }
        public string LastName { get; set; }
    }
}