using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryManagement.Models;

namespace LibraryManagement.Data.Interface
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User> GetUserByIdAsync(long userId);
        Task<IEnumerable<User>> GetUserByRoleAsync(long roleId);
        Task<User> CreateUserAsync(User user);
        Task UpdateUserAsync(User newUser);
        Task DeleteUserAsync(long userId);
        Task<User> AuthenticateUser(String userName, String password);
        Task<int> CountAllUsersAsync(string searchKeyWords = "");
        Task<IEnumerable<User>> GetUsersAsync(int page, int numPerPage, string searchKeyWords = "");
    }
}
