using LibraryManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.Data.Interface
{
    public interface IAuthorRepository
    {
        Task<IEnumerable<Author>> GetAllAuthorsAsync();
        Task<Author> GetAuthorByIdAsync(int authorId);
        Task CreateAuthorAsync(Author author);
        Task UpdateAuthorAsync(int authorId,Author author);
        Task DeleteAuthorAsync(int authorId);
        Task<int> CountAllAuthorsAsync(string searchKeyWords = "");
        Task<IEnumerable<Author>> GetAuthorsAsync(int page, int numPerPage, string searchKeyWords = "");
    }
}
