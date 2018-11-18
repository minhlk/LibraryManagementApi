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
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserService _userService;

        public UsersController(IUserRepository userRepository, IUserService userService)
        {
            _userRepository = userRepository;
            _userService = userService;
        }
        

        // GET: api/Users

        [HttpGet]
        [Authorize(Roles = "Admin,Librarian")]
        public async Task<IEnumerable<User>> GetUser()
        {
            return await _userRepository.GetAllUsersAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Librarian")]
        public async Task<IActionResult> GetUser([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userRepository.GetUserByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }
        // GET: api/Users/getByRole
        [HttpGet("getbyrole/{id}")]
        [Authorize(Roles = "Admin,Librarian")]
        public async Task<IEnumerable<User>> GetUserByRole([FromRoute] long id)
        {
            return await _userRepository.GetUserByRoleAsync(id);
        }


        // PUT: api/Users/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Librarian")]
        public async Task<IActionResult> PutUser([FromRoute] long id, [FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != user.Id)
            {
                return BadRequest();
            }

            await _userRepository.UpdateUserAsync(user);

           
            return Ok();
        }

        // POST: api/Users
//        [HttpPost]
//        public async Task<IActionResult> PostUser([FromBody] User user)
//        {
//            if (!ModelState.IsValid)
//            {
//                return BadRequest(ModelState);
//            }
//
//            await _userRepository.CreateUserAsync(user);
//
//            return CreatedAtAction("GetUser", new { id = user.Id }, user);
//        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,Librarian")]
        public async Task<IActionResult> DeleteUser([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userRepository.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            await _userRepository.DeleteUserAsync(id);
            
            return Ok(user);
        }
        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login([FromBody]User userInfo)
        {
            var user = _userService.Authenticate(userInfo.UserName, userInfo.Password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect", status = 400 });

           
            return Ok(new {message = "Login success" ,status = 200 , result = user});
        }
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]User userInfo)
        {
            var user = await _userService.Register(userInfo);
            if(user == null)
                return BadRequest(new { message = "Your username already existed" ,status = 400});
            return Ok(new { message = "Account is created", status = 200 });
        }

    }
}