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
    public class GenreFilmController : ControllerBase
    {
        private readonly IGenreFilmService _service;

        public GenreFilmController(IGenreFilmService service) { _service = service; }

        // GET: api/GenreFilm
        [HttpGet]
        public async Task<ActionResult<List<GenreFilmDTO>>> GetGenreFilms()
        {
            return await _service.GetGenreFilmAsync();
        }

        // GET: api/GenreFilm/films/5
        [HttpGet("film/{filmId:int}")]
        public async Task<ActionResult<List<GenreFilmDTO>>> GetGenreFilmsByFilmId(int filmId)
        {
            return await _service.GetGenreFilmByFilmIdAsync(filmId);
        }

        // GET: api/GenreFilm/genres/3
        [HttpGet("genre/{genreId:int}")]
        public async Task<ActionResult<List<GenreFilmDTO>>> GetGenreFilmsByGenreId(int genreId)
        {
            return await _service.GetGenreFilmByGenreIdAsync(genreId);
        }

        // POST: api/GenreFilm
        [HttpPost]
        public async Task<ActionResult<GenreFilmDTO>> PostGenreFilm(GenreFilmDTO GenreFilm)
        {
            if (await _service.AnyGenreFilmAsync(x => x.FilmId == GenreFilm.FilmId &&
                                                             x.GenreId == GenreFilm.GenreId))
                return BadRequest("GenreFilm with that identities already existing");

            await _service.PostGenreFilmAsync(GenreFilm);

            return CreatedAtAction(
                nameof(GetGenreFilms),
                new { filmId = GenreFilm.FilmId, genreId = GenreFilm.GenreId },
                GenreFilm);
        }
        
        // PUT: api/GenreFilm
        [HttpPut]
        public async Task<IActionResult> PutGenreFilmAsync(GenreFilmDTO GenreFilm)
        {
            await _service.PutGenreFilmAsync(GenreFilm);
            return NoContent();
        }

        // DELETE: api/GenreFilm?filmId=3&genreId=5
        [HttpDelete]
        public async Task<IActionResult> DeleteGenreFilm(int filmId, int genreId)
        {
            if (!await _service.AnyGenreFilmAsync(x => x.FilmId == filmId &&
                                                              x.GenreId == genreId))
                return NotFound();

            await _service.DeleteGenreFilmAsync(filmId, genreId);

            return NoContent();
        }
    }
}
