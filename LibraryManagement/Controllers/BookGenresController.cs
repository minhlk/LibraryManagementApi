using System;
using LibraryManagement.Data.Interface;
using LibraryManagement.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using LibraryManagement.Services;
using Microsoft.AspNetCore.Authorization;

namespace LibraryManagement.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BookGenresController : ControllerBase
    {
        private readonly IBookGenreRepository _bookGenreRepository;

        public BookGenresController(IBookGenreRepository bookGenreRepository)
        {
            _bookGenreRepository = bookGenreRepository;
        }


        // GET: api/BookGenres
        [AllowAnonymous]
        [HttpGet]
        public async Task<IEnumerable<BookGenre>> GetBookGenre()
        {
            return await _bookGenreRepository.GetAllBookGenresAsync();
        }

        // GET: api/BookGenres/5
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookGenre([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var bookGenre = await _bookGenreRepository.GetBookGenreByIdAsync(id);

            if (bookGenre == null)
            {
                return NotFound(new { message = "Can't find this book genre", status = 400 });
            }

            return Ok(new { message = "Success", status = 200, result = bookGenre });
        }

        // PUT: api/BookGenres/5
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBookGenre([FromRoute] int id, [FromBody] BookGenre bookGenre)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != bookGenre.Id)
            {
                return NotFound(new { message = "Can't update this book genre", status = 400 });
            }

            await _bookGenreRepository.UpdateBookGenreAsync(id, bookGenre);

//            return Ok(new { message = "Save Success", status = 200, result = "" });
            return NoContent();
        }

        // POST: api/BookGenres
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> PostBookGenre([FromBody] BookGenre bookGenre)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _bookGenreRepository.CreateBookGenreAsync(bookGenre);

            return RedirectToAction("GetBookGenre", new { id = bookGenre.Id });
        }

        // DELETE: api/BookGenres/5
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBookGenre([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var bookGenre = await _bookGenreRepository.GetBookGenreByIdAsync(id);
            if (bookGenre == null)
            {
                return NotFound(new { message = "Not found this genre book", status = 400 });
            }

            await _bookGenreRepository.DeleteBookGenreAsync(id);

            return Ok(new { message = "Delete Success", status = 200, result = bookGenre });
        }



    }
}