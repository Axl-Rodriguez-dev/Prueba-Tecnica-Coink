namespace API.DTOs;

public class UserStoredProcResult
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public int CountryId { get; set; }
    public int DepartmentId { get; set; }
    public int MunicipalityId { get; set; }
    public string Direction { get; set; } = string.Empty;
    public string CountryName { get; set; } = string.Empty;
    public string DepartmentName { get; set; } = string.Empty;
    public string MunicipalityName { get; set; } = string.Empty;
}