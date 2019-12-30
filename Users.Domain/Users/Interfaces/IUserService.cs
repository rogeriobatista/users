using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Users.Domain.Users.Dtos;

namespace Users.Domain.Users.Interfaces
{
    public interface IUserService
    {
        Task<List<UserDto>> Get();
        Task<UserDto> Get(long id);
        Task<UserDto> Save(UserDto userDto);
        Task Delete(long id);
    }
}
