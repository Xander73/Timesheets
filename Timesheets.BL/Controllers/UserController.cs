using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Timesheets.DB.Services.Interfaces;

namespace Timesheets.BL.Controllers
{    
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public sealed class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }


        //[AllowAnonymous]
        //[HttpPost("authenticate")]
        //public IActionResult Authenticate([FromQuery] string user, string password)
        //{
        //    TokenResponse token = _userService.Authenticate(user, password);
        //    if (token is null)
        //    {
        //        return BadRequest(new { message = "Username or password is incorrect" });
        //    }
        //    SetTokenCookie(token.RefreshToken);
        //    return Ok(token);
        //}


        //[AllowAnonymous]
        //[HttpPost("refresh-token")]
        //public IActionResult Refresh()
        //{
        //    string oldRefreshToken = Request.Cookies["refreshToken"];
        //    string newRefreshToken = _userService.RefreshToken(oldRefreshToken);

        //    if (string.IsNullOrWhiteSpace(newRefreshToken))
        //    {
        //        return Unauthorized(new { message = "Invalid token" });
        //    }
        //    SetTokenCookie(newRefreshToken);
        //    return Ok(newRefreshToken);
        //}

        private void SetTokenCookie(string token)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(7)
            };
            Response.Cookies.Append("refreshToken", token, cookieOptions);
        }


        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }


        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}


        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}


        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}


        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
