using System;
using LibraryManagement.Data.Interface;
using LibraryManagement.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using LibraryManagement.Services;
using Microsoft.AspNetCore.Authorization;
using LibraryManagement.Config;

namespace LibraryManagement.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorRepository _authorRepository;

        public AuthorsController(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        [AllowAnonymous]
        [HttpGet("size")]
        public async Task<int> GetSize([FromQuery(Name = "searchKeyWords")] string searchKeyWords)
        {
            return await _authorRepository.CountAllAuthorsAsync(searchKeyWords ?? "");
        }
        [AllowAnonymous]
        [HttpGet("all")]
        public async Task<IEnumerable<Author>> GetAllAuthor()
        {
            return await _authorRepository.GetAllAuthorsAsync();
        }


        // GET: api/Authors
        [AllowAnonymous]
        [HttpGet]
        public async Task<IEnumerable<Author>> GetAuthor([FromQuery(Name = "page")] int page, [FromQuery(Name = "searchKeyWords")] string searchKeyWords)
        {
            return await _authorRepository.GetAuthorsAsync(page, GlobalVariables.PageSize, searchKeyWords ?? "");
        }
        // GET: api/Authors/5
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAuthor([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var author = await _authorRepository.GetAuthorByIdAsync(id);

            if (author == null)
            {
                return NotFound(new { message = "Not found this author", status = 400 });
            }

            return Ok(new { message = "Success", status = 200, result = author });
        }

        // PUT: api/Authors/5
        [Authorize(Roles = "Admin,Librarian")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAuthor([FromRoute] int id, [FromBody] Author author)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != author.Id)
            {
                return BadRequest();
            }

            await _authorRepository.UpdateAuthorAsync(id, author);


            return Ok(new { message = "Save Success", status = 200, result = "" });
        }

        // POST: api/Authors
        [Authorize(Roles = "Admin,Librarian")]
        [HttpPost]
        public async Task<IActionResult> PostAuthor([FromBody] Author author)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _authorRepository.CreateAuthorAsync(author);

            return RedirectToAction("GetAuthor", new { id = author.Id });
        }

        // DELETE: api/Authors/5
        [Authorize(Roles = "Admin,Librarian")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var author = await _authorRepository.GetAuthorByIdAsync(id);
            if (author == null)
            {
                return BadRequest(new { message = "Can't delete this author", status = 400 });
            }

            await _authorRepository.DeleteAuthorAsync(id);

            return Ok(new { message = "Delete Success", status = 200, result = author });
        }
    }
}