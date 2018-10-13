using System;

namespace LibraryManager.Models
{
    public class Rental
    {
        public int Id { get; set; }
        public DateTime Issued { get; set; }
        public DateTime Returned { get; set; }
        public User User { get; set; }
        public Book Book { get; set; }            
    }
}