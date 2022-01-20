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

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilmController : ControllerBase
    {
        private readonly IFilmService _service;

        public FilmController(IFilmService service) { _service = service; }

        // GET: api/filmes
        [HttpGet()]
        public async Task<ActionResult<List<FilmDTO>>> GetFilms() => await _service.GetFilmsAsync();

        // GET: api/filmes/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<FilmDTO>> GetFilm(int id)
        {
            var film = await _service.GetFilmAsync(id);

            if (film == null) return NotFound();

            return film;
        }

        // GET: api/films/avengers
        [HttpGet("{name}")]
        public async Task<ActionResult<List<FilmDTO>>> GetFilmsByName(string name)
        {
            return await _service.GetFilmsByNameAsync(name);
        }

        [HttpGet("Description/{description}")]
        public async Task<ActionResult<List<FilmDTO>>> GetFilmsByDescription(string description)
        {
            return await _service.GetFilmsByDescriptionAsync(description);
        }
        // GET: api/films/2021
        [HttpGet("Year/{year}")]
        public async Task<ActionResult<List<FilmDTO>>> GetFilmsByYear(int year)
        {
            return await _service.GetFilmsByYearAsync(year);
        }

        [HttpGet("Theme/{theme}")]
        public async Task<ActionResult<List<FilmDTO>>> GetFilmsByTheme(string theme)
        {
            return await _service.GetFilmsByThemeAsync(theme);
        }
        [HttpGet("Search/")]
        public async Task<ActionResult<List<FilmDTO>>> GetFilmsByParameter(
    
           [FromQuery] string? name, [FromQuery] string? description) =>

            await _service.GetFilmsByParameters(name, description);



        // PUT: api/filmes/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> PutFilmAsync(int id, FilmDTO film)
        {
            if (await _service.AnyFilmsAsync(c => c.Name == film.Name && c.Id != id))
                return BadRequest("Another film with that name already existing");

            if (id != film.Id)
                return BadRequest("film must have same in header and body");

            await _service.PutFilmAsync(id, film);
            return NoContent();
        }

        // POST: api/filmes
        [HttpPost]
        public async Task<ActionResult<FilmDTO>> PostFilm(FilmDTO film)
        {
            var allfilms = await _service.GetFilmsAsync();
            if (allfilms.Any(c => c.Name == film.Name))
                return BadRequest("film with that name already existing");
            // Make same with categoryId and cuisineId

            await _service.PostFilmAsync(film);

            return CreatedAtAction(nameof(GetFilm), new { id = film.Id }, film);
        }

        // DELETE: api/filmes/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteFilm(int id)
        {
            if (!await _service.AnyFilmsAsync(d => d.Id == id))
                return NotFound();

            await _service.DeleteFilmAsync(id);

            return NoContent();
        }
    }
}
