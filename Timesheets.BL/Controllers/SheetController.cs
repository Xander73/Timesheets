using Core.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Timesheets.DB.DAL.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Timesheets.BL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SheetController : ControllerBase
    {
        private ISheetRepo _repo;

        public SheetController(ISheetRepo repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IEnumerable<Sheet>> Get(CancellationToken token)
        {
            return await _repo.GetAll(token);
        }


        [HttpGet("{id}")]
        public async Task<Sheet> Get(Guid id, CancellationToken token)
        {
            return await _repo.Get(id, token);
        }


        [HttpPost]
        public async Task Post([FromBody] Sheet item, CancellationToken token)
        {
            await _repo.AddItem(item, token);
        }


        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }


        [HttpDelete("{id}")]
        public async Task Delete(Guid id, CancellationToken token)
        {
           await _repo.DeleteItem(id, token);
        }
    }
}
