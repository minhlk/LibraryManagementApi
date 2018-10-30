using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using LibraryManagement.Config;
using LibraryManagement.Data.Interface;
using LibraryManagement.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace LibraryManagement.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IOptions<AppSettings> _config;
        private readonly byte DefaultRole = 3;
        public UserService(IUserRepository userRepository,IOptions<AppSettings> config)
        {
            this._userRepository = userRepository;
            this._config = config;
        }
        public UserAuth Authenticate(string userName, string password)
        {
            //TODO : hash password here
           //  call user repository to confirm user
            var user = _userRepository.AuthenticateUser(userName, Password.EncryptString(_config.Value.SecretKey,password));
            if (user != null)
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_config.Value.Secret);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name,user.Result.Id.ToString()),
                        new Claim(ClaimTypes.Role, user.Result.IdRoleNavigation.RoleName), 
                        
                    }),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                    
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = tokenHandler.WriteToken(token);
                return new UserAuth()
                {

                    UserName = user.Result.UserName,
                    Name = user.Result.Name,
                    Phone = user.Result.Phone,
                    YearOfBirth = user.Result.YearOfBirth,
                    Token = tokenString
                };
            }

            return null;

        }

        public async Task<User> Register(User user)
        {
            //TODO : add logic check here 
            user.Password = Password.EncryptString(_config.Value.SecretKey, user.Password);
            user.IdRole = DefaultRole;
            return await _userRepository.CreateUserAsync(user);
        }

        public async Task<UserAuth> GetById(int userId)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);
            if(user.IdRole != null)
                return new UserAuth()
            {
                UserName = user.UserName,
                Name = user.Name,
                Phone = user.Phone,
                YearOfBirth = user.YearOfBirth
            };
            return null;
        }
    }
}
