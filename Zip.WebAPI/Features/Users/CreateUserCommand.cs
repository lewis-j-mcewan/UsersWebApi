using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using MediatR;
using Zip.WebAPI.Models;

namespace Zip.WebAPI.Features.Users;

public class CreateUserCommand : IRequest<UserDto>
{
    [Required]
    [DefaultValue("")]
    public string Name { get; set; }

    [Required]
    [DefaultValue("")]
    [RegularExpression("^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$", ErrorMessage = "This is not an email")]
    public string Email { get; set; } = "";

    [Required, Range(0, int.MaxValue, ErrorMessage = "Expenses must be a positive number")]
    [DefaultValue(0)]
    public decimal? Expenses { get; set; }

    [Required, Range(0, int.MaxValue, ErrorMessage = "Salary must be a positive number")]
    [DefaultValue(0)]
    public decimal? Salary { get; set; }
}