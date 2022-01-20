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
    public class AccountController : ControllerBase
    {
        private readonly IUserService _service;

        public AccountController(IUserService service) { _service = service; }

        [HttpPost("authenticate")]
        public async Task<ActionResult<LoginRes>> Authenticate(LoginReq model)
        {
            var response = await _service.AuthenticateAsync(model);

            if (response == null)
                return BadRequest("Username or password is incorrect");

            return Ok(response);
        }

        [HttpPost("register")]
        public async Task<ActionResult<LoginRes>> Register(UserDTO user)
        {
            var response = await _service.RegisterAsync(user);

            if (response == null)
                return BadRequest("Didn't register!");

            return Ok(response);
        }

        [HttpGet()]
        public async Task<ActionResult<List<UserInfoDTO>>> GetUsersInfo()
        {
            return Ok(await _service.GetUsersInfoAsync());
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<UserInfoDTO>> GetUserInfoByIdAsync(int id)
        {
            var user = await _service.GetUserInfoByIdAsync(id);

            if (user == null) return NotFound();

            return user;
        }
    }
}
