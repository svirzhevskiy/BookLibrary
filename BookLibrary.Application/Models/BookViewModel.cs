using System.Collections.Generic;

namespace BookLibrary.Application.Models
{
    public class BookViewModel
    {
        public string Time { get; set; }
        public IEnumerable<Book> Books { get; set; }
    }
}