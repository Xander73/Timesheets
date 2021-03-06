
﻿using Core.Model;
using Microsoft.AspNetCore.Mvc;
using Timesheets.DB.DAL.Interfaces;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Timesheets.DB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private IPersonRepo _personRepo;
        public PersonController(IPersonRepo personRepo)
        {
            _personRepo = personRepo;
        }


        [HttpGet()]
        public async Task<ActionResult<IEnumerable<Person>>> GetPersons(
            CancellationToken token)
        {
            return Ok(_personRepo.GetAll());
        }


        [HttpGet("persons/{id}")]
        public async Task<ActionResult<Person>> GetPersonId(
            [FromRoute] int id,
            CancellationToken token)
        {
            Person person = _personRepo.Get(id);

            if (person != null)
            {
                return Ok(person);
            }
            return NoContent();
        }


        //Из за этого такого типа запросв не запускается сваггер
        [HttpGet("persons/search?searchTerm={term}")]
        public async Task<ActionResult<Person>> GetPersonTerm(
            [FromQuery] string term,
            CancellationToken token)
        {
            Person person = _personRepo.GetAll().Where(x => x.FirstName == term).FirstOrDefault();

            if (person != null)
            {
                return Ok(person);
            }
            return NoContent();
        }


        //И этот тоже не запускается сваггер
        [HttpGet("persons/?skip={skip}&take={take}")]
        public async Task<ActionResult<IEnumerable<Person>>> GetSomePersons(
            [FromQuery] int skip,
            [FromQuery] int take,
            CancellationToken token)
        {
            IEnumerable<Person>  persons = _personRepo.GetSomePersons(skip, take);
            return Ok(persons);
            
        }


        [HttpPost("persons")]
        public async Task<ActionResult<int>> PostPerson(
            [FromBody] Person person,
            CancellationToken token)
        {
            int id = _personRepo.AddItem(person);

            return Ok(id);
        }


        [HttpPut("persons")]
        public async Task<ActionResult<Person>> PutPerson(
            [FromBody] Person person,
            CancellationToken token)
        {
            int id = _personRepo.UpdateItem(person);
            if (id != 0)
            {
                return Ok(id);
            }
            return Ok(id);
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<Person>> Delete([FromRoute] int id, CancellationToken token)
        {
            _personRepo.DeleteItem(id);

            return Ok();
        }
    }
}
 main
