using LibraryManagement.Data.Interface;
using LibraryManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Data
{
    public class AuthorRepository : RepositoryBase<Author>, IAuthorRepository
    {
        public AuthorRepository(LibraryManagementContext repositoryContext) : base(repositoryContext)
        {
        }
        public async Task<IEnumerable<Author>> GetAllAuthorsAsync()
        {
            var authors = await FindAllAsync();
            return authors.OrderBy(x => x.Name);
        }

        public async Task<int> CountAllAuthorsAsync(string searchKeyWords = "")
        {
            return await CountAll(author => searchKeyWords.Trim().Length == 0 || author.Name.ToLower().Contains(searchKeyWords.Trim().ToLower())).CountAsync();
        }

        public async Task<Author> GetAuthorByIdAsync(int authorId)
        {
            var author = await FindByConditionAsync(x => x.Id == authorId);
            return author.FirstOrDefault();
        }
        public async Task CreateAuthorAsync(Author author)
        {
            Create(author);
            await SaveAsync();
        }
        public async Task UpdateAuthorAsync(int aithorId, Author newAuthor)
        {
            var author = await GetAuthorByIdAsync(aithorId);
            author.Name = newAuthor.Name;
            author.YearOfBirth = newAuthor.YearOfBirth;
            Update(author);
            await SaveAsync();
        }
        public async Task DeleteAuthorAsync(int authorId)
        {
            var author = await GetAuthorByIdAsync(authorId);
            Delete(author);
            await SaveAsync();
        }

        public async Task<IEnumerable<Author>> GetAuthorsAsync(int page, int numPerPage, string searchKeyWords = "")
        {
            return await this.FindByCondition(author => searchKeyWords.Trim().Length == 0 || author.Name.ToLower().Contains(searchKeyWords.Trim().ToLower()))
                .Skip(page * numPerPage)
                .Take(numPerPage)
                .ToListAsync();
        }
    }
}
