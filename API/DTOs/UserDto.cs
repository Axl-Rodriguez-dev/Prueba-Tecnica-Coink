namespace API.DTOs;

public class UserDto
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Phone { get; set; }
    public required string Country { get; set; }
    public required string Department { get; set; }
    public required string Municipality { get; set; }
    public required string Direction { get; set; }
}
