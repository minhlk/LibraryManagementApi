using LibraryManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.Services
{
    public interface IUserBookService
    {
        Task<ResultModel<UserBook>> AddUserBook(UserBook userBook);
        Task<ResultModel<UserBook>> EditUserBook(int id,UserBook userBook);
    }
}
