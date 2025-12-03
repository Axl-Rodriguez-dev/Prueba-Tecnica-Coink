namespace API.Entities;

public class AppUser
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Phone { get; set; }

    public int CountryId { get; set; }
    public int DepartmentId { get; set; }
    public int MunicipalityId { get; set; }

    public Country? Country { get; set; }
    public Department? Department { get; set; }
    public Municipality? Municipality { get; set; }
    public required string Direction { get; set; }
}
