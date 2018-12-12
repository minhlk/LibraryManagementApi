using System;
using LibraryManagement.Data.Interface;
using LibraryManagement.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using LibraryManagement.Config;
using LibraryManagement.Services;
using Microsoft.AspNetCore.Authorization;

namespace LibraryManagement.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;
        private readonly IUserService _userService;

        public BooksController(IBookRepository bookRepository, IUserService userService)
        {
            _bookRepository = bookRepository;
            _userService = userService;
        }


        [AllowAnonymous]
        [HttpGet("size")]
        public async Task<int> GetSize([FromQuery(Name = "searchKeyWords")] string searchKeyWords)
        {
            return await _bookRepository.CountAllBooksAsync(searchKeyWords ?? "");/// GlobalVariables.PageSize;
        }
        // GET: api/Books
        [AllowAnonymous]
        [HttpGet]
        public async Task<IEnumerable<Book>> GetBook([FromQuery(Name = "page")] int page, [FromQuery(Name = "searchKeyWords")] string searchKeyWords, [FromQuery(Name = "genre")]string genre, [FromQuery(Name = "author")]string author)
        {
            return await _bookRepository.GetBooksAsync(page, GlobalVariables.PageSize, searchKeyWords ?? "", genre ?? "", author ?? "");
        }
        // GET: api/Books/5
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBook([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var book = await _bookRepository.GetBookByIdAsync(id);

            if (book == null)
            {
                return NotFound(new { message = "Can't find this book", status = 400 });
            }

            return Ok(new { message = "Success", status = 200, result = book });
        }
        // GET: api/Books/list
        [AllowAnonymous]
        [HttpGet("list")]
        public async Task<IEnumerable<Book>> GetBookList()
        {
            return await _bookRepository.GetListAsync();
        }

        // PUT: api/Books/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutBook([FromRoute] long id, [FromBody] Book book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != book.Id)
            {
                return NotFound(new { message = "Can't update this book", status = 400 });
            }

            await _bookRepository.UpdateBookAsync(book);


            return Ok(new { message = "Save Success", status = 200, result = "" });
        }

        // POST: api/Books
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PostBook([FromBody] Book book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _bookRepository.CreateBookAsync(book);

            return RedirectToAction("GetBook", new { id = book.Id });
        }

        // DELETE: api/Books/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteBook([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var book = await _bookRepository.GetBookByIdAsync(id);
            if (book == null)
            {
                return BadRequest(new { message = "Can't delete this book", status = 400 });
            }

            await _bookRepository.DeleteBookAsync(id);

            return Ok(new { message = "Delete Success", status = 200, result = book });
        }



    }
}