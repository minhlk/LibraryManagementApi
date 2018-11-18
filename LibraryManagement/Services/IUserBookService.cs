using LibraryManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.Services
{
    public interface IUserBookService
    {
        Task<IEnumerable<UserBook>> GetAll();
        Task<IEnumerable<UserBook>> GetUserBookNullEndDate();
    }
}
