using API.Entities;

namespace API.Interfaces;

public interface IUserService
{
    Task<AppUser> CreateUserAsync(AppUser user);
    Task<IReadOnlyList<AppUser>> GetAllUsersAsync();
}
