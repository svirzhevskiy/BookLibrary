using System;

namespace BookLibrary.Database.Entities
{
    public class Book
    {
        public Guid Id { get; set; }
        public string ISBN { get; set; }
        public string Title { get; set; }
        public ushort Year { get; set; }
        public string Blurb { get; set; }
        
        public Guid AuthorId { get; set; }
        public Author Author { get; set; }
        
        public Guid PublisherId { get; set; }
        public Publisher Publisher { get; set; }
    }
}