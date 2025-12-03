using API.Entities;

namespace API.Interfaces;

public interface IMunicipalityService
{
    Task<IReadOnlyList<Municipality>> GetAllAsync();
}
