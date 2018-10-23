using LibraryManagement.Data.Interface;
using LibraryManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.Data
{
    public class BookGenreRepository : RepositoryBase<BookGenre>, IBookGenreRepository
    {
        public BookGenreRepository(LibraryManagementContext repositoryContext) : base(repositoryContext)
        {
        }
        public async Task<IEnumerable<BookGenre>> GetAllBookGenresAsync()
        {
            var bookGenres = await FindAllAsync();
            return bookGenres;
        }
        public async Task<BookGenre> GetBookGenreByIdAsync(int bookGenreId)
        {
            var bookGenre = await FindByConditionAsync(x => x.Id == bookGenreId);
            return bookGenre.FirstOrDefault();
        }
        public async Task CreateBookGenreAsync(BookGenre bookGenre)
        {
            Create(bookGenre);
            await SaveAsync();
        }
        public async Task UpdateBookGenreAsync(int aithorId, BookGenre newBookGenre)
        {
            var bookGenre = await GetBookGenreByIdAsync(aithorId);
            bookGenre.IdBook = newBookGenre.IdBook;
            bookGenre.IdGenre = newBookGenre.IdGenre;
            Update(bookGenre);
            await SaveAsync();
        }
        public async Task DeleteBookGenreAsync(int bookGenreId)
        {
            var bookGenre = await GetBookGenreByIdAsync(bookGenreId);
            Delete(bookGenre);
            await SaveAsync();
        }
    }
}
