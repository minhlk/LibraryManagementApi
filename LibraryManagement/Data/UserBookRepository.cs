using LibraryManagement.Data.Interface;
using LibraryManagement.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.Data
{
    public class UserBookRepository : RepositoryBase<UserBook>, IUserBookRepository
    {
        public UserBookRepository(LibraryManagementContext repositoryContext) : base(repositoryContext)
        {
        }
        public async Task<IEnumerable<UserBook>> GetAllUserBooksAsync()
        {
            var userBooks = await GetAll()
                .Include(x => x.IdUserNavigation)
                .Include(x => x.IdBookNavigation)
                .ToListAsync();
            return userBooks;
        }
        public async Task<IEnumerable<UserBook>> GetUserBookNullEndDate()
        {
            var userBooks = await GetMany(n => n.EndDate == null)
                .Include(x => x.IdUserNavigation)
                .Include(x => x.IdBookNavigation)
                .ToListAsync();
            return userBooks;
        }
        public async Task<UserBook> GetUserBookByIdAsync(int userBookId)
        {
            var userBook = await FindByConditionAsync(x => x.Id == userBookId);
            return userBook.FirstOrDefault();
        }
        public async Task CreateUserBookAsync(UserBook userBook)
        {
            Create(userBook);
            await SaveAsync();
        }
        public async Task UpdateUserBookAsync(int aithorId, UserBook newUserBook)
        {
            var userBook = await GetUserBookByIdAsync(aithorId);
            userBook.IdUser = newUserBook.IdUser;
            userBook.IdBook = newUserBook.IdBook;
            userBook.StartDate = newUserBook.StartDate;
            userBook.EndDate = newUserBook.EndDate;
            userBook.NumberOfDays = newUserBook.NumberOfDays;
            Update(userBook);
            await SaveAsync();
        }
        public async Task DeleteUserBookAsync(int userBookId)
        {
            var userBook = await GetUserBookByIdAsync(userBookId);
            Delete(userBook);
            await SaveAsync();
        }
    }
}
