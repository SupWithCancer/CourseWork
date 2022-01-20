using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Services;
using BusinessLogicLayer.DTOs;
using DataAccessLayer.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService _service;

        public PersonController(IPersonService service) { _service = service; }

        // GET: api/Persones
        [HttpGet()]
        public async Task<ActionResult<List<PersonDTO>>> GetPersons() => await _service.GetPersonsAsync();

        // GET: api/Persones/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<PersonDTO>> GetPerson(int id)
        {
            var Person = await _service.GetPersonAsync(id);

            if (Person == null) return NotFound();

            return Person;
        }

  
        // PUT: api/Persones/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> PutPersonAsync(int id, PersonDTO Person)
        {
            if (await _service.AnyPersonsAsync(c => c.Name == Person.Name && c.Id != id))
                return BadRequest("Another Person with that name already existing");

            if (id != Person.Id)
                return BadRequest("Person must have same in header and body");

            await _service.PutPersonAsync(id, Person);
            return NoContent();
        }


        // GET: api/filmes/avengers
        [HttpGet("{name}")]
        public async Task<ActionResult<List<PersonDTO>>> GetPersonsByName(string name)
        {
            return await _service.GetPersonsByNameAsync(name);
        }


        // POST: api/Persones
        [HttpPost]
        public async Task<ActionResult<PersonDTO>> PostPerson(PersonDTO Person)
        {
            var allPersons = await _service.GetPersonsAsync();
            if (allPersons.Any(c => c.Name == Person.Name))
                return BadRequest("Person with that name already existing");
            // Make same with categoryId and cuisineId

            await _service.PostPersonAsync(Person);

            return CreatedAtAction(nameof(GetPerson), new { id = Person.Id }, Person);
        }

        // DELETE: api/Persones/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeletePerson(int id)
        {
            if (!await _service.AnyPersonsAsync(d => d.Id == id))
                return NotFound();

            await _service.DeletePersonAsync(id);

            return NoContent();
        }
    }
}
