using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using LibraryManagement.Data.Interface;
using LibraryManagement.Models;
using Microsoft.IdentityModel.Tokens;

namespace LibraryManagement.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            this._userRepository = userRepository;
        }
        public UserAuth Authenticate(string userName, string password)
        {
            //TODO : hash password here
           //  call user repository to confirm this
            var user = _userRepository.AuthenticateUser(userName, password);
            if (user != null)
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes("this is my custom Secret key for authnetication");
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name,user.Id.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                    
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = tokenHandler.WriteToken(token);
                return new UserAuth()
                {

                    UserName = user.UserName,
                    Name = user.Name,
                    Phone = user.Phone,
                    YearOfBirth = user.YearOfBirth,
                    Token = tokenString
                };
            }

            return null;

        }

        public async Task Register(User user)
        {
            
            //TODO : add logic check here and hash password
             await _userRepository.CreateUserAsync(user);
        }

        public async Task<UserAuth> GetById(int userId)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);
            //TODO: add get service later
            return new UserAuth()
            {
                UserName = user.UserName,
                Name = user.Name,
                Phone = user.Phone,
                YearOfBirth = user.YearOfBirth
            };
        }
    }
}
