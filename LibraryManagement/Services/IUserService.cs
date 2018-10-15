using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryManagement.Models;

namespace LibraryManagement.Services
{
    public interface IUserService
    {
        UserAuth Authenticate(String userName, String password);
        Task Register(User user);
        Task<UserAuth> GetById(int userId);
    }

    public class UserAuth
    {
        public string UserName { get; set; }
        public string Name { get; set; }
        public string YearOfBirth { get; set; }
        public string Phone { get; set; }
        public string Token { get; set; }

    }

}
