using API.Data;
using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Services;

public class DepartmentService(AppDbContext context) : IDepartmentService
{
    public async Task<IReadOnlyList<Department>> GetAllAsync()
    {
        var results = await context.Database
            .SqlQueryRaw<Department>("SELECT * FROM sp_get_all_departments()")
            .ToListAsync();
        return results;
    }
}
