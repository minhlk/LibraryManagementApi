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
    public class UserBooksController : ControllerBase
    {
        private readonly IUserBookRepository _userBookRepository;
        private readonly IUserBookService _userBookService;

        public UserBooksController(IUserBookRepository userBookRepository, IUserBookService userBookService)
        {
            _userBookRepository = userBookRepository;
            _userBookService = userBookService;
        }


        // GET: api/UserBooks
        [AllowAnonymous]
        [HttpGet]
        public async Task<IEnumerable<UserBook>> GetUserBook()
        {
            return await _userBookService.GetAll();
        }

        // GET: api/UserBooks/lending
        [AllowAnonymous]
        [HttpGet("lending")]
        public async Task<IEnumerable<UserBook>> GetUserBookNullEndDate()
        {
            return await _userBookService.GetUserBookNullEndDate();
        }

        // GET: api/UserBooks/5
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserBook([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userBook = await _userBookRepository.GetUserBookByIdAsync(id);

            if (userBook == null)
            {
                return NotFound(new { message = "Can't find this book", status = 400 });
            }

            return Ok(new { message = "Success", status = 200, result = userBook });
        }

        // PUT: api/UserBooks/5
        [Authorize(Roles = "Admin,Librarian")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserBook([FromRoute] int id, [FromBody] UserBook userBook)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != userBook.Id)
            {
                return NotFound(new { message = "Can't update this book", status = 400 });
            }

            await _userBookRepository.UpdateUserBookAsync(id, userBook);


            return Ok(new { message = "Save Success", status = 200, result = "" });
        }

        // POST: api/UserBooks
        [Authorize(Roles = "Admin,Librarian")]
        [HttpPost]
        public async Task<IActionResult> PostUserBook([FromBody] UserBook userBook)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _userBookRepository.CreateUserBookAsync(userBook);

            return RedirectToAction("GetUserBook", new { id = userBook.Id });
        }

        // DELETE: api/UserBooks/5
        [Authorize(Roles = "Admin,Librarian")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserBook([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userBook = await _userBookRepository.GetUserBookByIdAsync(id);
            if (userBook == null)
            {
                return BadRequest(new { message = "Can't delete this user book", status = 400 });
            }

            await _userBookRepository.DeleteUserBookAsync(id);

            return Ok(new { message = "Delete Success", status = 200, result = userBook });
        }



    }
}