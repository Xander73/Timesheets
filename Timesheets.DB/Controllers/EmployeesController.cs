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
    public class EmployeesController : ControllerBase
    {
<<<<<<< HEAD
        private readonly MyDbContext _context;

        public EmployeesController(MyDbContext context)
        {
            _context = context;
        }
=======
        private readonly IEmployeeRepo _repo;
        private IUserService _userService;

        public EmployeesController(IEmployeeRepo repo, IUserService userService)
        {
            _repo = repo;
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

>>>>>>> f379f9d (Add authorization and authotication)

        // GET: api/Employees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees(CancellationToken token)
        {
            return await _context.Employees.ToListAsync();
        }

        // GET: api/Employees/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployee(Guid id, CancellationToken token)
        {
            var employee = await _context.Employees.FindAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            return employee;
        }

        // PUT: api/Employees/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee(Guid id, Employee employee, CancellationToken token)
        {
            if (id != employee.Id)
            {
                return BadRequest();
            }

            _context.Entry(employee).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok();
        }

        // POST: api/Employees
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Employee>> PostEmployee(Employee employee, CancellationToken token)
        {
            employee.Id = Guid.NewGuid();
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEmployee", new { id = employee.Id }, employee);
        }

        // DELETE: api/Employees/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(Guid id, CancellationToken token)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();

            return Ok();
        }

        private bool EmployeeExists(Guid id)
        {
            return _context.Employees.Any(e => e.Id == id);
        }
    }
}
