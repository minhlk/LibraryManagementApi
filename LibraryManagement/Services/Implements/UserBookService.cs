using LibraryManagement.Data.Interface;
using LibraryManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.Services.Implements
{
    public class UserBookService : IUserBookService
    {
        private readonly IUserBookRepository _userBookRepository;
        public UserBookService(IUserBookRepository userBookRepository)
        {
            this._userBookRepository = userBookRepository;
        }
        public async Task<IEnumerable<UserBook>> GetAll()
        {
            var userBooks = await _userBookRepository.GetAllUserBooksAsync();
            return userBooks;
        }
        public async Task<IEnumerable<UserBook>> GetUserBookNullEndDate()
        {
            var userBooks = await _userBookRepository.GetUserBookNullEndDate();
            return userBooks;
        }
    }
}
