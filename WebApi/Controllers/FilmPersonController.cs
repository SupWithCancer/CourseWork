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
    public class FilmPersonController : ControllerBase
    {
        private readonly IFilmPersonService _service;

        public FilmPersonController(IFilmPersonService service) { _service = service; }

        // GET: api/FilmPerson
        [HttpGet]
        public async Task<ActionResult<List<FilmPersonDTO>>> GetFilmPersons()
        {
            return await _service.GetFilmPersonAsync();
        }

        // GET: api/FilmPerson/Film/5
        [HttpGet("Film/{filmId:int}")]
        public async Task<ActionResult<List<FilmPersonDTO>>> GetFilmPersonsByFilmId(int filmId)
        {
            return await _service.GetFilmPersonByFilmIdAsync(filmId);
        }

        // GET: api/FilmPerson/Person/3
        [HttpGet("Person/{personId:int}")]
        public async Task<ActionResult<List<FilmPersonDTO>>> GetFilmPersonsByPersonId(int personId)
        {
            return await _service.GetFilmPersonByPersonIdAsync(personId);
        }

        // POST: api/FilmPerson
        [HttpPost]
        public async Task<ActionResult<FilmPersonDTO>> PostFilmPerson(FilmPersonDTO FilmPerson)
        {
            if (await _service.AnyFilmPersonAsync(x => x.FilmId == FilmPerson.FilmId &&
                                                             x.PersonId == FilmPerson.PersonId &&
                                                             x.RoleId == FilmPerson.RoleId))
                return BadRequest("FilmPerson with that identities already existing");

            await _service.PostFilmPersonAsync(FilmPerson);

            return CreatedAtAction(
                nameof(GetFilmPersons),
                new { filmId = FilmPerson.FilmId, personId = FilmPerson.PersonId, roleId = FilmPerson.RoleId },
                FilmPerson);
        }
        
        // PUT: api/FilmPerson
        [HttpPut]
        public async Task<IActionResult> PutFilmPersonAsync(FilmPersonDTO FilmPerson)
        {
            await _service.PutFilmPersonAsync(FilmPerson);
            return NoContent();
        }

        // DELETE: api/FilmPerson?personId=3&filmId=5
        [HttpDelete]
        public async Task<IActionResult> DeleteFilmPerson(int filmId, int personId, int roleId)
        {
            if (!await _service.AnyFilmPersonAsync(x => x.FilmId == filmId &&
                                                              x.PersonId == personId &&
                                                              x.RoleId == roleId))
                return NotFound();

            await _service.DeleteFilmPersonAsync(filmId, personId, roleId);

            return NoContent();
        }
    }
}
