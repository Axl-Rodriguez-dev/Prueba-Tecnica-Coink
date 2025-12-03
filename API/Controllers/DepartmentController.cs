using API.Entities;
using API.Interfaces;
using API.DTOs;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class DepartmentController(IDepartmentService departmentService, IMapper mapper) : BaseApiController
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<DepartmentDto>>> GetAll()
    {
        var entities = await departmentService.GetAllAsync();
        var result = mapper.Map<IReadOnlyList<DepartmentDto>>(entities);
        return Ok(result);
    }
}
