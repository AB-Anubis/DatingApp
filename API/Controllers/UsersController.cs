using System.Security.Claims;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("API/[controller]")]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userrepository;
        private readonly IMapper _mapper;

        public UsersController(IUserRepository userrepository, IMapper mapper)
        {
            _mapper = mapper;
            _userrepository = userrepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers()
        {
            var users = await _userrepository.GetMembersAsync();
            return Ok(users);
        }

        
        [HttpGet("{username}")]
        public async Task<ActionResult<MemberDto>> GetUser(string username)
        {
            return await _userrepository.GetMemberAsync(username);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateUser(MemberUpdateDto memberUpdateDto)
        {
            var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = await _userrepository.GetUserByUsernameAsync(username);

            _mapper.Map(memberUpdateDto, user);

            _userrepository.Update(user);

            if (await _userrepository.SaveAllAsync()) return NoContent();
            return BadRequest("Failed to update user!");
        }
    }
}