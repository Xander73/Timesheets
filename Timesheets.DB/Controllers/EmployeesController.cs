#nullable disable
using Microsoft.AspNetCore.Mvc;
using Core.Models.Entities;
using Timesheets.DB.DAL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Timesheets.DB.Services.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Timesheets.DB.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeRepo _repo;

        public EmployeesController(IEmployeeRepo repo)
        {
            _repo = repo;
        }


        private void SetTokenCookie([MinLength(10), StringLength(100)] string token)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(7)
            };
            Response.Cookies.Append("refreshToken", token, cookieOptions);
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees(CancellationToken token)
        {
            IEnumerable<Employee> employee = await _repo.GetAll(token);
            if (employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployee(Guid id, CancellationToken token)
        {
            var employee = _repo.Get;

            if (employee == null)
            {
                return NotFound();
            }

            return Ok(employee);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee(Guid id, Employee employee, CancellationToken token)
        {
            if (id != employee.Id)
            {
                return NotFound();
            }

            await _repo.UpdateItem(employee, token);

            return Ok();
        }


        [HttpPost]
        public async Task<ActionResult<Employee>> PostUser(Employee employee, CancellationToken token)
        {
            employee.Id = Guid.NewGuid();
            await _repo.AddItem(employee, token);

            return Ok();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(Guid id, CancellationToken token)
        {
            var employeeEntity = await _repo.Get(id, token);
            if (employeeEntity == null)
            {
                return NotFound();
            }
            await _repo.DeleteItem(id, token);
            return Ok();
        }
    }
}
