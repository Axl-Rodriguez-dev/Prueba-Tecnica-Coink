using API.Entities;

namespace API.Interfaces;

public interface ICountryService
{
    Task<IReadOnlyList<Country>> GetAllAsync();
}
