using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using LibraryManagement.Data.Interface;
using LibraryManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Data
{
    public class BookRepository : RepositoryBase<Book>, IBookRepository
    {
        public BookRepository(LibraryManagementContext repositoryContext) : base(repositoryContext)
        {
        }

        public async Task<IEnumerable<Book>> GetAllBooksAsync()
        {

            var books = await FindAllAsync();
            return books.OrderBy(x => x.Name);

        }
        public async Task<int> CountAllBooksAsync(string searchKeyWords = "")
        {
            return await CountAll(book => searchKeyWords.Trim().Length == 0 || book.Name.ToLower().Contains(searchKeyWords.Trim().ToLower())).CountAsync();
        }
        public async Task<IEnumerable<Book>> GetBooksAsync(int page, int numPerPage, string searchKeyWords = "")
        {
            return await this.FindByCondition(book => searchKeyWords.Trim().Length == 0 || book.Name.ToLower().Contains(searchKeyWords.Trim().ToLower()))
                .Include(b => b.IdAuthorNavigation)
                .Include(b => b.BookGenre)
                .ThenInclude(b => b.IdGenreNavigation)
                .Skip(page * numPerPage)
                .Take(numPerPage)
                .ToListAsync();
        }
        public async Task<IEnumerable<Book>> GetListAsync()
        {
            var books = await this.GetAllBooksAsync();
            var result = books.Select(p => new Book
            {
                //Add more properties if need
                Id = p.Id,
                Name = p.Name,
            }).ToList();
            return result;
        }

        public async Task<Book> GetBookByIdAsync(long bookId)
        {
            var book = await FindByConditionAsync(x => x.Id == bookId);
            var rs = book.FirstOrDefault();
            //Get Details about genres
            if (rs != null)
                rs.BookGenre = await RepositoryContext.BookGenre.Where(b => b.IdBook == bookId).ToListAsync();
            return rs;
        }

        public async Task CreateBookAsync(Book book)
        {
            Create(book);
            await SaveAsync();
        }

        public async Task UpdateBookAsync(Book newBook)
        {
            var book = await GetBookByIdAsync(newBook.Id);
            book.Map(newBook);
            Update(book);
            await SaveAsync();
        }

        public async Task DeleteBookAsync(long bookId)
        {
            var book = await GetBookByIdAsync(bookId);
            Delete(book);
            await SaveAsync();
        }

    }
    public static class Extension
    {
        public static void Map(this Book oldBook, Book newBook)
        {
            //Check this later on
            oldBook.Name = newBook.Name;
            oldBook.Amount = newBook.Amount;
            oldBook.IdAuthor = newBook.IdAuthor;
            oldBook.BookGenre = newBook.BookGenre;
            oldBook.Image = newBook.Image;

        }
    }
}
