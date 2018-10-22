using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using LibraryManagement.Data.Interface;
using LibraryManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Data
{
    public class GenreRepository : RepositoryBase<Genre>,IGenreRepository
    {
        public GenreRepository(LibraryManagementContext repositoryContext) : base(repositoryContext)
        {
        }

        public async Task<IEnumerable<Genre>> GetAllGenresAsync()
        {

            var genres = await FindAllAsync();
            return genres.OrderBy(x => x.Name);

        }
        public async Task<Genre> GetGenreByIdAsync(long genreId)
        {
            var genre = await FindByConditionAsync(x => x.Id == genreId);
            var rs = genre.FirstOrDefault();
            //Get Details about genres
            rs.BookGenre = await RepositoryContext.BookGenre.Where(b => b.IdGenre == genreId).ToListAsync();
            return rs;
        }

        public async Task CreateGenreAsync(Genre genre)
        {
            Create(genre);
            await SaveAsync();
        }

        public async Task UpdateGenreAsync(Genre newGenre)
        {
            var genre = await GetGenreByIdAsync(newGenre.Id);
            genre.Name = newGenre.Name;
            Update(genre);
            await SaveAsync();
        }

        public async Task DeleteGenreAsync(long genreId)
        {
            var genre = await GetGenreByIdAsync(genreId);
            Delete(genre);
            await SaveAsync();
        }

    }
   

}
