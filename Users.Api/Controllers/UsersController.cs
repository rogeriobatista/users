using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Users.Domain.Users.Dtos;
using Users.Domain.Users.Interfaces;
using Users.Infra.Data.Context;

namespace Users.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UsersDbContext _context;
        private readonly IUserService _userService;

        public UsersController(UsersDbContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        [HttpGet()]
        public async Task<ActionResult<List<UserDto>>> Get()
        {
            return Ok(await _userService.Get());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetById(long id)
        {
            return Ok(await _userService.Get(id));
        }

        [HttpPost]
        public async Task<ActionResult<UserDto>> Save([FromBody]UserDto userDto)
        {
            UserDto result = await _userService.Save(userDto);
            await _context.SaveChangesAsync();
            return Ok(result);
        }

        [HttpDelete]
        public async Task Delete(long id)
        {
            await _userService.Delete(id);
            await _context.SaveChangesAsync();
        }
    }
}