using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryManagement.Models;

namespace LibraryManagement.Data.Interface
{
    public interface IGenreRepository
    {
        Task<IEnumerable<Genre>> GetAllGenresAsync();
        Task<Genre> GetGenreByIdAsync(long genreId);
        Task CreateGenreAsync(Genre genre);
        Task UpdateGenreAsync(Genre newGenre);
        Task DeleteGenreAsync(long genreId);
        Task<IEnumerable<Genre>> GetGenresAsync(int page, int numPerPage, string searchKeyWords = "");
        Task<int> CountAllGenresAsync(string searchKeyWords = "");
    }
}
