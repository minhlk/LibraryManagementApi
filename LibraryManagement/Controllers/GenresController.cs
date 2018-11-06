using LibraryManagement.Data.Interface;
using LibraryManagement.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using LibraryManagement.Config;
using Microsoft.AspNetCore.Authorization;

namespace LibraryManagement.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly IGenreRepository _genreRepository;

        public GenresController(IGenreRepository genreRepository)
        {
            _genreRepository = genreRepository;
        }

        [AllowAnonymous]
        [HttpGet("size")]
        public async Task<int> GetSize([FromQuery(Name = "searchKeyWords")] string searchKeyWords)
        {
            return await _genreRepository.CountAllGenresAsync(searchKeyWords ?? "");
        }
        [AllowAnonymous]
        [HttpGet("all")]
        public async Task<IEnumerable<Genre>> GetAllGenre()
        {
            return await _genreRepository.GetAllGenresAsync();
        }

        // GET: api/Genres
        [AllowAnonymous]
        [HttpGet]
        public async Task<IEnumerable<Genre>> GetGenre([FromQuery(Name = "page")] int page, [FromQuery(Name = "searchKeyWords")] string searchKeyWords)
        {
            return await _genreRepository.GetGenresAsync(page, GlobalVariables.PageSize, searchKeyWords ?? "");
        }
        // GET: api/Genres/5
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetGenre([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var genre = await _genreRepository.GetGenreByIdAsync(id);

            if (genre == null)
            {
                return NotFound(new { message = "Not found this genre", status = 400 });
            }

            return Ok(new { message = "Success", status = 200, result = genre });
        }

        // PUT: api/Genres/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Librarian")]
        public async Task<IActionResult> PutGenre([FromRoute] long id, [FromBody] Genre genre)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != genre.Id)
            {
                return NotFound(new { message = "Can't update this genre", status = 400 });
            }

            await _genreRepository.UpdateGenreAsync(genre);


            return Ok(new { message = "Save Success", status = 200, result = "" });
        }

        // POST: api/Genres
        [HttpPost]
        [Authorize(Roles = "Admin,Librarian")]
        public async Task<IActionResult> PostGenre([FromBody] Genre genre)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _genreRepository.CreateGenreAsync(genre);

            return RedirectToAction("GetGenre", new { id = genre.Id });
        }

        // DELETE: api/Genres/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,Librarian")]
        public async Task<IActionResult> DeleteGenre([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var genre = await _genreRepository.GetGenreByIdAsync(id);
            if (genre == null)
            {
                return BadRequest(new { message = "Can't delete this genre", status = 400 });
            }

            await _genreRepository.DeleteGenreAsync(id);

            return Ok(new { message = "Delete Success", status = 200, result = genre });
        }
        
    }
}