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

        public UserBooksController(IUserBookRepository userBookRepository)
        {
            _userBookRepository = userBookRepository;
        }


        // GET: api/UserBooks
        [AllowAnonymous]
        [HttpGet]
        public async Task<IEnumerable<UserBook>> GetUserBook()
        {
            return await _userBookRepository.GetAllUserBooksAsync();
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
                return NotFound();
            }

            return Ok(userBook);
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
                return BadRequest();
            }

            await _userBookRepository.UpdateUserBookAsync(id, userBook);


            return NoContent();
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

            return CreatedAtAction("GetUserBook", new { id = userBook.Id }, userBook);
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
                return NotFound();
            }

            await _userBookRepository.DeleteUserBookAsync(id);

            return Ok(userBook);
        }



    }
}