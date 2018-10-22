using LibraryManagement.Data.Interface;
using LibraryManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            return authors;
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
    }
}
