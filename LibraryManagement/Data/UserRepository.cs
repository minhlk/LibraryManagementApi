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
    public class UserRepository : RepositoryBase<User>,IUserRepository
    {
        public UserRepository(LibraryManagementContext repositoryContext) : base(repositoryContext)
        {
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {

            var users = await FindAllAsync();
            return users.OrderBy(x => x.Name);

        }
        public async Task<User> GetUserByIdAsync(long userId)
        {
            var user = await FindByConditionAsync(x => x.Id == userId);
            var rs = user.FirstOrDefault();
            //Get Details about books
            if(rs !=null)
            rs.UserBook = await RepositoryContext.UserBook.Where(b => b.IdUser == userId).ToListAsync();
            return rs;
        }

        public async Task<User> CreateUserAsync(User user)
        {
            //Check if username exists 
            if (this.RepositoryContext.User.FirstOrDefault(u => u.UserName == user.UserName) == null)
            {
                Create(user);
                await SaveAsync();
                return user;
            }

            return null;


        }

        public async Task UpdateUserAsync( User newUser)
        {
            var user = await GetUserByIdAsync(newUser.Id);
            //TODO : update user password
            user.Name = newUser.Name;
            user.UserName = newUser.UserName;
            user.IdRole = newUser.IdRole;
            user.Phone = newUser.Phone;
            user.YearOfBirth = newUser.YearOfBirth;

            Update(user);
            await SaveAsync();
        }

        public async Task DeleteUserAsync(long userId)
        {
            var user = await GetUserByIdAsync(userId);
            Delete(user);
            await SaveAsync();
        }

        public async Task<User> AuthenticateUser(string userName, string password)
        {
            var user = await  FindByConditionAsync(u => u.Password == password && u.UserName == userName);
            var rs = user.FirstOrDefault();
            //Get Details about user role
            if (rs != null)
            {
                rs.IdRoleNavigation = RepositoryContext.Role.FirstOrDefault(a => a.Id == rs.IdRole);
                return rs;
            }

            return null;
        }

        public async Task<IEnumerable<User>> GetUsersAsync(int page, int numPerPage, string searchKeyWords = "")
        {
            return await this.FindByCondition(user => searchKeyWords.Trim().Length == 0 || user.Name.ToLower().Contains(searchKeyWords.Trim().ToLower()))
                .Skip(page * numPerPage)
                .Take(numPerPage)
                .ToListAsync();
        }

        public async Task<int> CountAllUsersAsync(string searchKeyWords = "")
        {
            return await CountAll(user => searchKeyWords.Trim().Length == 0 || user.Name.ToLower().Contains(searchKeyWords.Trim().ToLower())).CountAsync();
        }
    }


}
