using API.Entities;
using API.Interfaces;
using API.DTOs;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class CountryController(ICountryService countryService, IMapper mapper) : BaseApiController
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<CountryDto>>> GetAll()
    {
        var entities = await countryService.GetAllAsync();
        var result = mapper.Map<IReadOnlyList<CountryDto>>(entities);
        return Ok(result);
    }
}
