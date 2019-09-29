namespace LibraryManager.Api.Models.Dto
{
    public class ReviewOutputDto
    {
        public long Id { get; set; }
        public int Rate { get; set; }
        public string Comment { get; set; }
        public long UserId { get; set; }
    }
}