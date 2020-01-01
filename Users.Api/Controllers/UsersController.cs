using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Users.Domain.Users.Dtos;
using Users.Domain.Users.Interfaces;
using Users.Infra.Data.Context;

namespace Users.Api.Controllers
{
    [Authorize]
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

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            try
            {
                return Ok(await _userService.Get());
            }
            catch (System.Exception ex)
            {
                return Ok(ex);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(long id)
        {
            try
            {
                return Ok(await _userService.Get(id));
            }
            catch (System.Exception ex)
            {
                return Ok(ex);
            }
        }

        [HttpPatch]
        public async Task<ActionResult> Save([FromBody]UserDto userDto)
        {
            try
            {
                UserDto result = await _userService.Save(userDto);

                if (_context.ChangeTracker.HasChanges())
                    await _context.SaveChangesAsync();

                return Ok(result);
            }
            catch (System.Exception ex)
            {
                return Ok(ex);
            }            
        }

        [HttpDelete("{id}")]
        public async Task Delete(long id)
        {
            await _userService.Delete(id);
            await _context.SaveChangesAsync();
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] UserDto userDto)
        {
            try
            {
                UserDto result = await _userService.Save(userDto);

                if (_context.ChangeTracker.HasChanges())
                    await _context.SaveChangesAsync();

                return Ok(result);
            }
            catch (System.Exception ex)
            {
                return Ok(ex);
            }
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] UserDto userDto)
        {
            try
            {
                return Ok(await _userService.Login(userDto));
            }
            catch (System.Exception ex)
            {
                return Ok(ex);
            }            
        }

        [AllowAnonymous]
        [HttpPost("recover")]
        public async Task<ActionResult> Recover([FromBody] RecoverUserPasswordDto recoverDto)
        {
            try
            {
                string result = await _userService.Recover(recoverDto);

                if (_context.ChangeTracker.HasChanges())
                    await _context.SaveChangesAsync();

                return Ok(result);
            }
            catch (System.Exception ex)
            {
                return Ok(ex);
            }
        }
    }
}