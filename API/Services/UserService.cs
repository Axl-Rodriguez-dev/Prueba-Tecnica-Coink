using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace API.Services;

public class UserService(AppDbContext context) : IUserService
{
    public async Task<AppUser> CreateUserAsync(AppUser user)
    {
        var phoneExists = await context.Database
            .SqlQueryRaw<bool>(
                @"SELECT sp_phone_exists(@phone) AS ""Value""",
                new NpgsqlParameter("@phone", user.Phone)
            )
            .FirstOrDefaultAsync();
        if (phoneExists) throw new ArgumentException("El número de teléfono ya existe.");

        var countryExists = await context.Database
            .SqlQueryRaw<bool>(
                @"SELECT sp_country_exists(@countryId) AS ""Value""",
                new NpgsqlParameter("@countryId", user.CountryId)
            )
            .FirstOrDefaultAsync();
        if (!countryExists) throw new ArgumentException("El CountryId no existe.");

        var departmentExists = await context.Database
            .SqlQueryRaw<bool>(
                @"SELECT sp_department_exists(@departmentId) AS ""Value""",
                new NpgsqlParameter("@departmentId", user.DepartmentId)
            )
            .FirstOrDefaultAsync();
        if (!departmentExists) throw new ArgumentException("El DepartmentId no existe.");

        var municipalityExists = await context.Database
            .SqlQueryRaw<bool>(
                @"SELECT sp_municipality_exists(@municipalityId) AS ""Value""",
                new NpgsqlParameter("@municipalityId", user.MunicipalityId)
            )
            .FirstOrDefaultAsync();
        if (!municipalityExists) throw new ArgumentException("El MunicipalityId no existe.");

        var result = await context.Database
            .SqlQueryRaw<UserStoredProcResult>(@"
                SELECT * FROM sp_create_user(
                    @p_name, 
                    @p_phone, 
                    @p_country_id, 
                    @p_department_id, 
                    @p_municipality_id, 
                    @p_direction
                )",
                new NpgsqlParameter("@p_name", user.Name),
                new NpgsqlParameter("@p_phone", user.Phone),
                new NpgsqlParameter("@p_country_id", user.CountryId),
                new NpgsqlParameter("@p_department_id", user.DepartmentId),
                new NpgsqlParameter("@p_municipality_id", user.MunicipalityId),
                new NpgsqlParameter("@p_direction", user.Direction)
            )
            .FirstOrDefaultAsync() ?? throw new Exception("Error al crear el usuario.");

        return new AppUser
        {
            Id = result.Id,
            Name = result.Name,
            Phone = result.Phone,
            CountryId = result.CountryId,
            DepartmentId = result.DepartmentId,
            MunicipalityId = result.MunicipalityId,
            Direction = result.Direction,
            Country = new Country { Id = result.CountryId, Name = result.CountryName },
            Department = new Department { Id = result.DepartmentId, Name = result.DepartmentName },
            Municipality = new Municipality { Id = result.MunicipalityId, Name = result.MunicipalityName }
        };
    }

    public async Task<IReadOnlyList<AppUser>> GetAllUsersAsync()
    {
        var results = await context.Database
            .SqlQueryRaw<UserStoredProcResult>("SELECT * FROM sp_get_all_users()")
            .ToListAsync();

        return [.. results.Select(r => new AppUser
        {
            Id = r.Id,
            Name = r.Name,
            Phone = r.Phone,
            CountryId = r.CountryId,
            DepartmentId = r.DepartmentId,
            MunicipalityId = r.MunicipalityId,
            Direction = r.Direction,
            Country = new Country { Id = r.CountryId, Name = r.CountryName },
            Department = new Department { Id = r.DepartmentId, Name = r.DepartmentName },
            Municipality = new Municipality { Id = r.MunicipalityId, Name = r.MunicipalityName }
        })];
    }
}
