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
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _service;

        public CommentController(ICommentService service) { _service = service; }

        // GET: api/Commentes
        [HttpGet()]
        public async Task<ActionResult<List<CommentDTO>>> GetComments() => await _service.GetCommentsAsync();

        // GET: api/Commentes/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<CommentDTO>> GetComment(int id)
        {
            var Comment = await _service.GetCommentAsync(id);

            if (Comment == null) return NotFound();

            return Comment;
        }

        //// GET: api/Commentes/avengers
        //[HttpGet("{name:string}")]
        //public async Task<ActionResult<List<CommentDTO>>> GetCommentsByName(string name)
        //{
        //    return await _service.GetCommentsByNameAsync(name);
        //}

        // GET: api/Commentes/Film/3
        [HttpGet("Film/{filmId:int}")]
        public async Task<ActionResult<List<CommentDTO>>> GetCommentsByFilmId(int filmId)
        {
            return await _service.GetCommentsByFilmIdAsync(filmId);
        }


        // GET: api/Commentes/User/3
        [HttpGet("User/{Id:int}")]
        public async Task<ActionResult<List<CommentDTO>>> GetCommentsByUserId(int userId)
        {
            return await _service.GetCommentsByUserIdAsync(userId);
        }
        // PUT: api/Comments/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> PutCommentAsync(int id, CommentDTO Comment)
        {
           

            if (id != Comment.Id)
                return BadRequest("Comment must have same in header and body");

            await _service.PutCommentAsync(id, Comment);
            return NoContent();
        }

        // POST: api/Commentes
        [HttpPost]
        public async Task<ActionResult<CommentDTO>> PostComment(CommentDTO Comment)
        {
            var allComments = await _service.GetCommentsAsync();
           
            // Make same with categoryId and cuisineId

            await _service.PostCommentAsync(Comment);

            return CreatedAtAction(nameof(GetComment), new { id = Comment.Id }, Comment);
        }

        // DELETE: api/Commentes/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteComment(int id)
        {
            if (!await _service.AnyCommentsAsync(d => d.Id == id))
                return NotFound();

            await _service.DeleteCommentAsync(id);

            return NoContent();
        }
    }
}
