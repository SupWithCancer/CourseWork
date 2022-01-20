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
    public class MarkController : ControllerBase
    {
        private readonly IMarkService _service;

        public MarkController(IMarkService service) { _service = service; }

        // GET: api/Markes
        [HttpGet()]
        public async Task<ActionResult<List<MarkDTO>>> GetMarks() => await _service.GetMarksAsync();

        // GET: api/Markes/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<MarkDTO>> GetMark(int id)
        {
            var Mark = await _service.GetMarkAsync(id);

            if (Mark == null) return NotFound();

            return Mark;
        }

        //// GET: api/Markes/avengers
        //[HttpGet("{name:string}")]
        //public async Task<ActionResult<List<MarkDTO>>> GetMarksByName(string name)
        //{
        //    return await _service.GetMarksByNameAsync(name);
        //}

        // GET: api/Markes/Film/3
        [HttpGet("Film/{filmId:int}")]
        public async Task<ActionResult<List<MarkDTO>>> GetMarksByFilmId(int filmId)
        {
            return await _service.GetMarksByFilmIdAsync(filmId);
        }


        // GET: api/Markes/User/3
        [HttpGet("Users/{userId:int}")]
        public async Task<ActionResult<List<MarkDTO>>> GetMarksByUserId(int userId)
        {
            return await _service.GetMarksByUserIdAsync(userId);
        }



        // POST: api/Grades
        [HttpPost]
        public async Task<ActionResult<MarkDTO>> PostGrade(MarkDTO mark)
        {
            if (await _service.AnyMarksAsync(x => x.FilmId == mark.FilmId &&
                                                      x.UserId == mark.UserId))
                return BadRequest("Mark with that identities already existing");

            await _service.PostMarkAsync(mark);

            return CreatedAtAction(nameof(GetMarks),
                new { markId = mark.FilmId, userId = mark.UserId }, mark);
        }

            // DELETE: api/Markes/5
            [HttpDelete("{id:int}")]
            public async Task<IActionResult> DeleteMark(int id)
            {
                if (!await _service.AnyMarksAsync(d => d.Id == id))
                    return NotFound();

                await _service.DeleteMarkAsync(id);

                return NoContent();
            }
        }
    } 
