using LibraryManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.Data.Interface
{
    public interface IUserBookRepository
    {
        Task<IEnumerable<UserBook>> GetAllUserBooksAsync();
        Task<UserBook> GetUserBookByIdAsync(int userBookId);
        Task CreateUserBookAsync(UserBook userBook);
        Task UpdateUserBookAsync(int userBookId, UserBook userBook);
        Task DeleteUserBookAsync(int userBookId);
        Task<IEnumerable<UserBook>> GetUserBookNullEndDate(int page, int numPerPage, string searchKeyWords = "");
        Task<IEnumerable<UserBook>> GetUserBooksAsync(int page, int numPerPage, string searchKeyWords = "");
        Task<int> CountAllUserBooksAsync(string searchKeyWords = "");
        Task<int> CountAllUserBooksNullEndDateAsync(string searchKeyWords = "");
    }
}
