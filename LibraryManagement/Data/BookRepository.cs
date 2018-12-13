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
            var result = await this.FindByCondition(book => searchKeyWords.Trim().Length == 0 || book.Name.ToLower().Contains(searchKeyWords.Trim().ToLower()))
                .Include(b => b.IdAuthorNavigation)
                .Include(b => b.BookGenre)
                .ThenInclude(b => b.IdGenreNavigation)
                .Skip(page * numPerPage)
                .Take(numPerPage)
                .ToListAsync();
            return result;
        }
        public async Task<Object> GetBooksByConditionAsync(int page, int numPerPage, string searchKeyWords = "", string genre = "", string author = "")
        {
            var count = 0;
            var result = await this.FindByCondition(book => searchKeyWords.Trim().Length == 0 || book.Name.ToLower().Contains(searchKeyWords.Trim().ToLower()))
                .Where(o => string.IsNullOrEmpty(author) || o.IdAuthor == Convert.ToInt64(author))
                .Include(b => b.IdAuthorNavigation)
                .Include(b => b.BookGenre)
                .ToListAsync();
            result = result.Where(o => string.IsNullOrEmpty(genre) || o.BookGenre.Select(l => l.IdGenre).Contains(Convert.ToInt64(genre))).ToList();
            count = result.Count();
            result = result.Skip(page * numPerPage)
                            .Take(numPerPage)
                            .ToList();
            var obj = new
            {
                entities = result,
                count = count
            };
            return obj;
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
            var book = await FindByCondition(x => x.Id == bookId)
                .Include(b => b.IdAuthorNavigation)
                .ToListAsync();
            var rs = book.FirstOrDefault();
            //Get Details about genres
            if (rs != null)
                rs.BookGenre = await RepositoryContext.BookGenre.Where(b => b.IdBook == bookId)
                    .Include(a=> a.IdGenreNavigation)
                    .ToListAsync();
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
            oldBook.Description = newBook.Description;

        }
    }
}
