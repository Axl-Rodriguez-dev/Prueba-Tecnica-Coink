using API.Entities;

namespace API.Interfaces;

public interface IDepartmentService
{
    Task<IReadOnlyList<Department>> GetAllAsync();
}
