#nullable disable
using Microsoft.AspNetCore.Mvc;
using Core.Models.Entities;
using Timesheets.DB.DAL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Timesheets.DB.Services.Interfaces;

namespace Timesheets.DB.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepo _repo;
        private IUserService _userService;

        public UsersController(IUserRepo repo, IUserService userService)
        {
            _repo = repo;
            _userService = userService;

        }


        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromQuery] string user, string password, CancellationToken tokenCancellation)
        {
            TokenResponse token = _userService.Authenticate(user, password, tokenCancellation);
            if (token is null)
            {
                return BadRequest(new { message = "Username or password is incorrect" });
            }
            SetTokenCookie(token.RefreshToken);

            return Ok(token);
        }


        [AllowAnonymous]
        [HttpPost("refresh-token")]
        public IActionResult Refresh(CancellationToken tokenCancellation)
        {
            string oldRefreshToken = Request.Cookies["refreshToken"];
            string newRefreshToken = _userService.RefreshToken(oldRefreshToken, tokenCancellation);

            if (string.IsNullOrWhiteSpace(newRefreshToken))
            {
                return Unauthorized(new { message = "Invalid token" });
            }
            SetTokenCookie(newRefreshToken);
            return Ok(newRefreshToken);
        }


        private void SetTokenCookie(string token)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(7)
            };
            Response.Cookies.Append("refreshToken", token, cookieOptions);
        }


        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers(CancellationToken token)
        {
            IEnumerable<User> users = await _repo.GetAll(token);
            if (users == null)
            {
                return NotFound();
            }
            return Ok(users);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(Guid id, CancellationToken token)
        {
            var user = _repo.Get(id, token);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(Guid id, User user, CancellationToken token)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            await _repo.UpdateItem(user, token);

            return Ok();
        }


        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user, CancellationToken token)
        {
            user.Id = Guid.NewGuid();
            await _repo.AddItem(user, token);

            return Ok();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id, CancellationToken token)
        {
            var userEntity = await _repo.Get(id, token);
            if (userEntity == null)
            {
                return NotFound();
            }
            await _repo.DeleteItem(id, token);
            return Ok();
        }
    }
}
