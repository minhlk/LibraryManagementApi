using LibraryManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.Data.Interface
{
    public interface IBookGenreRepository
    {
        Task<IEnumerable<BookGenre>> GetAllBookGenresAsync();
        Task<BookGenre> GetBookGenreByIdAsync(int bookGenreId);
        Task CreateBookGenreAsync(BookGenre bookGenre);
        Task UpdateBookGenreAsync(int bookGenreId, BookGenre bookGenre);
        Task DeleteBookGenreAsync(int bookGenreId);
    }
}
