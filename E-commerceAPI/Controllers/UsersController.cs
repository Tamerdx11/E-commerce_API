using E_commerceAPI.Models;
using E_commerceAPI.Repository.Base;
using E_commerceAPI.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_commerceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUser _user;
        private IConfiguration _configuration;
        public UsersController(IUser user, IConfiguration configuration)
        {
            _user = user;
            _configuration = configuration;
        }

        [HttpGet("All")]
        [Authorize(Roles = "Admin")]
        public ActionResult<IEnumerable<User>> Index()
        {
            return Ok(_user.GetAll());
        }
        [HttpGet("Details/{email}")]
        [Authorize(Roles = "User, Admin")]
        public async Task<ActionResult<User>> GetUserDtails(string email)
        {
            return Ok( await _user.GetByEmail(email));
        }

        [HttpPost("Register")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public ActionResult<User> Register([FromBody] UserDto userDto)
        {
            if (userDto is null)
                return BadRequest();

            string passwordHash = BCrypt.Net.BCrypt.HashPassword(userDto.Password);

            User user = new User {
                Name = userDto.Name,
                Email = userDto.Email,
                PasswordHash = passwordHash,
                IsAdmin = userDto.IsAdmin
            };

            _user.Add(user);

            return CreatedAtAction(nameof(GetUserDtails), new { email = user.Email}, user);
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<User>> Login(UserLoginDto userLoginDto)
        {
            if (userLoginDto is null)
                return BadRequest();

            User user =await _user.GetByEmail(userLoginDto.Email.ToString());

            if (user is null)
                return BadRequest("Email does not exist");

            if (!BCrypt.Net.BCrypt.Verify(userLoginDto.Password, user.PasswordHash))
                return BadRequest("Wrong password");

            return Ok(CreateToken.Create(user, _configuration));
        }

    }
}
