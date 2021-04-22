using System.Collections.Generic;
using System.Threading.Tasks;
using BookLibrary.Application.Models;

namespace BookLibrary.Application.Services
{
    public interface IBookService
    {
        Task<BookViewModel> Search(BookFilter filter);
    }
}