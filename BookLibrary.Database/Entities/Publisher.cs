using System;
using System.Collections.Generic;

namespace BookLibrary.Database.Entities
{
    public class Publisher
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        
        public IEnumerable<Book> Books { get; set; }
    }
}