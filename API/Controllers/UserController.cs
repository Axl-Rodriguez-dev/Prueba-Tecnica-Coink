using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using API.DTOs;
using AutoMapper;

namespace API.Controllers;

public class UserController(IUserService userService, IMapper mapper) : BaseApiController
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<UserDto>>> GetMembers()
    {
        var members = await userService.GetAllUsersAsync();
        var result = mapper.Map<IReadOnlyList<UserDto>>(members);
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<UserDto>> CreateUser([FromBody] CreateUserDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var entity = mapper.Map<AppUser>(dto);
        var created = await userService.CreateUserAsync(entity);
        var result = mapper.Map<UserDto>(created);

        return CreatedAtAction(nameof(GetMembers), new { id = result.Id }, result);
    }
}
