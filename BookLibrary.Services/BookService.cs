using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BookLibrary.Application.Models;
using BookLibrary.Application.Services;
using BookLibrary.Database;
using Microsoft.EntityFrameworkCore;
using NpgsqlTypes;
using Book = BookLibrary.Application.Models.Book;

namespace BookLibrary.Services
{
    public class BookService : IBookService
    {
        private readonly AppDbContext _context;

        public BookService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<BookViewModel> Search(BookFilter filter)
        {
            var watch = new Stopwatch();
            watch.Start();

            var result = await _context.Books.Where(x => 
                    EF.Functions.ToTsVector("english", x.Title + " " + x.Blurb).Matches(filter.SearchString))
                .Select(x => new
                {
                    Title = x.Title,
                    ISBN = x.ISBN,
                    Year = x.Year,
                    Blurb = x.Blurb,
                    Author = x.Author.Name,
                    Publisher = x.Publisher.Title
                })
                .ToListAsync();

            watch.Stop();
            var elapsedTime = watch.Elapsed;

            return new BookViewModel
            {
                Books = result.Select(x => new Book
                {
                    Title = x.Title,
                    ISBN = x.ISBN,
                    Year = x.Year,
                    Blurb = x.Blurb,
                    Author = x.Author,
                    Publisher = x.Publisher
                }),
                Time = $"{elapsedTime.Seconds : 00}s {elapsedTime.Milliseconds : 00}ms"
            };
        }
    }
}