namespace Zip.WebAPI.Models;

public class UserDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public decimal? Expenses { get; set; }
    public decimal? Salary { get; set; }
}