#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Core.Models.Entities;
<<<<<<< HEAD
using Timesheets.DB.DAL.Context;
=======
using Timesheets.DB.DAL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Timesheets.DB.Services.Interfaces;
>>>>>>> f379f9d (Add authorization and authotication)

namespace Timesheets.DB.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class UsersController : ControllerBase
    {
<<<<<<< HEAD
        private readonly MyDbContext _context;

        public UsersController(MyDbContext context)
        {
            _context = context;
        }

        // GET: api/Users
=======
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
>>>>>>> f379f9d (Add authorization and authotication)
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers(CancellationToken token)
        {
            return await _context.Users.ToListAsync();
        }

<<<<<<< HEAD
        // GET: api/Users/5
=======

>>>>>>> f379f9d (Add authorization and authotication)
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(Guid id, CancellationToken token)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

<<<<<<< HEAD
        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
=======

>>>>>>> f379f9d (Add authorization and authotication)
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(Guid id, User user, CancellationToken token)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

<<<<<<< HEAD
            _context.Entry(user).State = EntityState.Modified;
=======
            await _repo.UpdateItem(user, token);
>>>>>>> f379f9d (Add authorization and authotication)

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

<<<<<<< HEAD
        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
=======

        [AllowAnonymous]
>>>>>>> f379f9d (Add authorization and authotication)
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user, CancellationToken token)
        {
            user.Id = Guid.NewGuid();
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }

<<<<<<< HEAD
        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id, CancellationToken token)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
=======

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id, CancellationToken token)
        {
            var userEntity = await _repo.Get(id, token);
            if (userEntity == null)
>>>>>>> f379f9d (Add authorization and authotication)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(Guid id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
