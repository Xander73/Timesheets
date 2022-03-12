#nullable disable
using Microsoft.AspNetCore.Mvc;
using Core.Models.Entities;
using Timesheets.DB.DAL.Interfaces;

namespace Timesheets.DB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepo _repo;

        public UsersController(IUserRepo repo)
        {
            _repo = repo;
        }

        
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
            var user = _repo.Get;

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
