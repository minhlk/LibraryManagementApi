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
        private readonly IBookRepository _bookRepository;
        public UserBookService(IUserBookRepository userBookRepository, IBookRepository bookRepository)
        {
            _userBookRepository = userBookRepository;
            _bookRepository = bookRepository;
        }
        public async Task<ResultModel<UserBook>> AddUserBook(UserBook userBook)
        {
            var book = await _bookRepository.GetBookByIdAsync(userBook.IdBook);
            if (book != null)
            {
                if (book.Amount > 0)
                {
                    book.Amount--;
                    await _userBookRepository.CreateUserBookAsync(userBook);
                    await _bookRepository.UpdateBookAsync(book);
                    return new ResultModel<UserBook>
                    {
                        Data = null,
                        Success = true,
                        Message = ""
                    };
                }
                else
                {
                    return new ResultModel<UserBook>
                    {
                        Data = null,
                        Success = false,
                        Message = "The amount is not enough"
                    };
                }
            }
            else
            {
                return new ResultModel<UserBook>
                {
                    Data = null,
                    Success = false,
                    Message = "Can't find the book"
                };
            }
        }
        public async Task<ResultModel<UserBook>> EditUserBook(int id, UserBook userBook)
        {
            var book = await _bookRepository.GetBookByIdAsync(userBook.IdBook);
            var userBookFromDB = await _userBookRepository.GetUserBookByIdAsync(id);
            if (book != null)
            {
                book.Amount++;
                await _userBookRepository.UpdateUserBookAsync(id, userBook);
                await _bookRepository.UpdateBookAsync(book);
                return new ResultModel<UserBook>
                {
                    Data = null,
                    Success = true,
                    Message = ""
                };
            }
            else
            {
                return new ResultModel<UserBook>
                {
                    Data = null,
                    Success = false,
                    Message = "Can't find the book"
                };
            }
        }
    }
}
