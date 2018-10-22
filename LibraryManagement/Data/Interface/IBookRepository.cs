using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryManagement.Models;

namespace LibraryManagement.Data.Interface
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetAllBooksAsync();
        Task<Book> GetBookByIdAsync(int bookId);
//        Task<BookExtended> GetBookWithDetailsAsync(Guid BookId);
        Task CreateBookAsync(Book book);
        Task UpdateBookAsync(Book newBook);
        Task DeleteBookAsync(int bookId);
    }
}
