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

        //// GET: api/filmes/Categories/3
        //[HttpGet("Categories/{categoryId:int}")]
        //public async Task<ActionResult<List<FilmDTO>>> GetfilmesByCategoryId(int categoryId)
        //{
        //    return await _service.GetfilmesByCategoryIdAsync(categoryId);
        //}

        //// GET: api/filmes/Cuisine/3
        //[HttpGet("Cuisines/{cuisineId:int}")]
        //public async Task<ActionResult<List<FilmDTO>>> GetfilmesByCuisineId(int cuisineId)
        //{
        //    return await _service.GetfilmesByCuisineIdAsync(cuisineId);
        //}

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
