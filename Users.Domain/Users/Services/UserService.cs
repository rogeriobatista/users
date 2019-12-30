using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Users.Domain.Users.Dtos;
using Users.Domain.Users.Entities;
using Users.Domain.Users.Interfaces;

namespace Users.Domain.Users.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserDto> Get(long id)
        {
            return UserDto.CreateUserDto(await _userRepository.GetByIdAsync(id));
        }

        public async Task<List<UserDto>> Get()
        {
            return _userRepository.ToListAsync().Result.Select(x => UserDto.CreateUserDto(x)).ToList();
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

            if (user != null)
                _userRepository.Remove(user);
        }

        private async Task<UserDto> Update(UserDto userDto)
        {
            User user = await _userRepository.GetByIdAsync(userDto.Id);

            user.UpdateUsername(userDto.Username);
            user.UpdatePassword(userDto.Password);
            user.UpdateName(userDto.Name);

            if (!user.Validate())
                return null;

            _userRepository.Update(user);

            return userDto;
        }

        private async Task<UserDto> Create(UserDto userDto)
        {
            User user = new User(userDto.Username, userDto.Password, userDto.Name);

            if (!user.Validate())
                return null;

            await _userRepository.AddAsync(user);

            return UserDto.CreateUserDto(user);
        }
    }
}
