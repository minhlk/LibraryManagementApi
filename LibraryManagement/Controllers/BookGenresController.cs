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
                return NotFound();
            }

            return Ok(bookGenre);
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
                return BadRequest();
            }

            await _bookGenreRepository.UpdateBookGenreAsync(id, bookGenre);


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

            return CreatedAtAction("GetBookGenre", new { id = bookGenre.Id }, bookGenre);
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
                return NotFound();
            }

            await _bookGenreRepository.DeleteBookGenreAsync(id);

            return Ok(bookGenre);
        }



    }
}