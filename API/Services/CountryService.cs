using API.Data;
using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Services;

public class CountryService(AppDbContext context) : ICountryService
{
    public async Task<IReadOnlyList<Country>> GetAllAsync()
    {
        var results = await context.Database
            .SqlQueryRaw<Country>("SELECT * FROM sp_get_all_countries()")
            .ToListAsync();
        return results;
    }
}
