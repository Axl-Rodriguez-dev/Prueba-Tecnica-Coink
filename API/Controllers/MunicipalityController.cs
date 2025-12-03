using API.Entities;
using API.Interfaces;
using API.DTOs;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class MunicipalityController(IMunicipalityService municipalityService, IMapper mapper) : BaseApiController
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<MunicipalityDto>>> GetAll()
    {
        var entities = await municipalityService.GetAllAsync();
        var result = mapper.Map<IReadOnlyList<MunicipalityDto>>(entities);
        return Ok(result);
    }
}
