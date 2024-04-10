using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using MediatR;
using Zip.WebAPI.Models;

namespace Zip.WebAPI.Features.Accounts;

public class CreateAccountCommand : IRequest<AccountDto>
{
    [Required, Range(1, int.MaxValue, ErrorMessage = "The user ID must be a positive integer")]
    [DefaultValue(1)]
    public int UserId { get; set; }
    [DefaultValue("")]
    public string Description { get; set; }
    
}