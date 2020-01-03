using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Users.Domain.Users.Dtos;
using Users.Domain.Users.Interfaces;
using Users.Generics.Interfaces;

namespace Users.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            try
            {
                return Ok(await _userService.Get());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(long id)
        {
            try
            {
                return Ok(await _userService.Get(id));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPatch]
        public async Task<ActionResult> Save([FromBody]UserDto userDto)
        {
            try
            {
                return Ok(await _userService.Save(userDto));
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }

        [HttpDelete("{id}")]
        public async Task Delete(long id)
        {
            await _userService.Delete(id);
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] UserDto userDto)
        {
            try
            {
                return Ok(await _userService.Save(userDto));
            }
            catch (Exception ex)
            {
                throw ex;
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
            catch (Exception ex)
            {
                throw ex;
            }            
        }

        [AllowAnonymous]
        [HttpPost("recover/{token}")]
        public async Task<ActionResult> Recover([FromServices] ITokenHelper tokenHelper, [FromBody] RecoverUserPasswordDto recoverDto, string token)
        {
            try
            {
                if (string.IsNullOrEmpty(token) || !tokenHelper.Validate(token))
                    return Unauthorized();

                return Ok(await _userService.Recover(recoverDto));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}