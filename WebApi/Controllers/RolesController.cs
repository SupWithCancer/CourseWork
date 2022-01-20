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
    public class RolesController : ControllerBase
    {
        private readonly IRoleService _service;

        public RolesController(IRoleService service) { _service = service; }

        // GET: api/Roles
        [HttpGet]
        public async Task<ActionResult<List<RoleDTO>>> GetRoles() => await _service.GetRolesAsync();

        // GET: api/Roles/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<RoleDTO>> GetRole(int id)
        {
            var Role = await _service.GetRoleAsync(id);

            if (Role == null) return NotFound();

            return Role;
        }

        //// GET: api/Roles/Groups/3
        //[HttpGet("Groups/{groupId:int}")]
        //public async Task<ActionResult<List<RoleDTO>>> GetRolesByGroupId(int groupId)
        //{
        //    return await _service.GetRolesByGroupIdAsync(groupId);
        //}
    }
}
