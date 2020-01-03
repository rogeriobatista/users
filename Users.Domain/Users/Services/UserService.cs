using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Users.Domain.Users.Dtos;
using Users.Domain.Users.Entities;
using Users.Domain.Users.Interfaces;
using Users.Generics.Helpers;
using Users.Generics.Resources;

namespace Users.Domain.Users.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly AppSettings _appSettings;

        public UserService(IUserRepository userRepository, IOptions<AppSettings> appSettings)
        {
            _userRepository = userRepository;
            _appSettings = appSettings.Value;
        }

        public async Task<UserDto> Get(long id)
        {
            return UserDto.CreateUserDto(await _userRepository.GetByIdAsync(id));
        }

        public async Task<List<UserDto>> Get()
        {
            return _userRepository.ToListAsync().Result.Select(UserDto.CreateUserDto).ToList();
        }

        public async Task<UserDto> Save(UserDto userDto)
        {
            if (userDto.Id > 0)
                return await Update(userDto);

            return await Create(userDto);
        }

        public async Task Delete(long id)
        {
            User user = await _userRepository.GetByIdAsync(id);

            if (user == null)
                throw new ArgumentNullException(StringResource.ValidationMessageUserDontExists);
            
            _userRepository.Remove(user);
        }

        public async Task<UserDto> Login(UserDto userDto)
        {
            string username = await _userRepository.Login(userDto);

            if (username == null)
                throw new ArgumentNullException(StringResource.ValidationMessageUserDontExists);

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, username)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            userDto.Token = tokenHandler.WriteToken(token);

            return userDto;
        }

        public async Task<string> Recover(RecoverUserPasswordDto recoverDto)
        {
            if (!await _userRepository.Exist(recoverDto.Email))
                throw new Exception(StringResource.ValidationMessageUserDontExists);

            User user = await _userRepository.GetByEmailAsync(recoverDto.Email);

            user.UpdatePassword(recoverDto.Password);

            if (!user.Validate())
                throw new ArgumentException(StringResource.ValidationMessageInvalidUser);

            _userRepository.Update(user);

            return StringResource.RecoverMessageSuccess;
        }

        private async Task<UserDto> Update(UserDto userDto)
        {
            User user = await _userRepository.GetByIdAsync(userDto.Id);

            if (user == null)
                throw new Exception(StringResource.ValidationMessageUserAlreadyExist);

            user.UpdateEmail(userDto.Email);
            user.UpdatePassword(userDto.Password);
            user.UpdateName(userDto.Name);

            if (!user.Validate())
                throw new ArgumentException(StringResource.ValidationMessageInvalidUser);

            _userRepository.Update(user);

            return userDto;
        }

        private async Task<UserDto> Create(UserDto userDto)
        {
            if (await _userRepository.Exist(userDto.Email))          
                throw new Exception(StringResource.ValidationMessageUserAlreadyExist);

            User user = new User(userDto.Email, userDto.Password, userDto.Name);

            if (!user.Validate())
                throw new ArgumentException(StringResource.ValidationMessageInvalidUser);

            await _userRepository.AddAsync(user);

            return UserDto.CreateUserDto(user);
        }
    }
}
