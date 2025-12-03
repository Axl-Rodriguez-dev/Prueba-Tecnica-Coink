using API.Data;
using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Services;

public class MunicipalityService(AppDbContext context) : IMunicipalityService
{
    public async Task<IReadOnlyList<Municipality>> GetAllAsync()
    {
        var results = await context.Database
            .SqlQueryRaw<Municipality>("SELECT * FROM sp_get_all_municipalities()")
            .ToListAsync();
        return results;
    }
}
