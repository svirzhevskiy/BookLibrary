using System.Collections.Generic;

namespace BookLibrary.Application.Models
{
    public class BookViewModel
    {
        public double Time { get; set; }
        public IEnumerable<Book> Books { get; set; }
    }
}