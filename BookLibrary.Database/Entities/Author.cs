using System;
using System.Collections.Generic;

namespace BookLibrary.Database.Entities
{
    public class Author
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        
        public IEnumerable<Book> Books { get; set; }
    }
}