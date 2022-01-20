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
using Microsoft.AspNetCore.Authorization;
using DataAccessLayer.Models;
namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly IGenreService _service;

        public GenresController(IGenreService service) { _service = service; }

        // GET: api/Genres
        [HttpGet]
        public async Task<ActionResult<List<GenreDTO>>> GetGenres() => await _service.GetGenresAsync();

        // GET: api/Genres/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<GenreDTO>> GetGenre(int id)
        {
            var Genre = await _service.GetGenreAsync(id);

            if (Genre == null) return NotFound();

            return Genre;
        }

        //// GET: api/Genres/Groups/3
        //[HttpGet("Groups/{groupId:int}")]
        //public async Task<ActionResult<List<GenreDTO>>> GetGenresByGroupId(int groupId)
        //{
        //    return await _service.GetGenresByGroupIdAsync(groupId);
        //}
    }
}
