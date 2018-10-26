﻿using System;
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
        public async Task<int> GetSize()
        {
            return await _bookRepository.CountAllBooksAsync();/// GlobalVariables.PageSize;
        }
        // GET: api/Books
        [AllowAnonymous]
        [HttpGet]
        public async Task<IEnumerable<Book>> GetBook([FromQuery(Name = "page")] int page)
        {
            return await _bookRepository.GetBooksByPageAsync(page,GlobalVariables.PageSize);
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
                return NotFound();
            }

            return Ok(book);
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
                return BadRequest();
            }

            await _bookRepository.UpdateBookAsync(book);

           
            return Ok();
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

            return CreatedAtAction("GetBook", new { id = book.Id }, book);
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
                return NotFound();
            }

            await _bookRepository.DeleteBookAsync(id);
            
            return Ok(book);
        }
      


    }
}