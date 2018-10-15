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
        Task<Genre> GetGenreByIdAsync(int genreId);
        Task CreateGenreAsync(Genre genre);
        Task UpdateGenreAsync(int genreId, Genre newGenre);
        Task DeleteGenreAsync(int genreId);
    }
}
