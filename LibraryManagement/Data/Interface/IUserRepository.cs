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
        Task<User> GetUserByIdAsync(int userId);
        Task<User> CreateUserAsync(User user);
        Task UpdateUserAsync(int userId, User newUser);
        Task DeleteUserAsync(int userId);
        User AuthenticateUser(String userName, String password);
    }
}
