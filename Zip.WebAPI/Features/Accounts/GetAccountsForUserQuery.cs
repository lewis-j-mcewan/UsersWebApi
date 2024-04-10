using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using MediatR;
using Zip.WebAPI.Models;

namespace Zip.WebAPI.Features.Accounts;

public class GetAccountsForUserQuery : IRequest<IEnumerable<AccountDto>>
{
    [Required, Range(1, int.MaxValue, ErrorMessage = "UserId must be a positive integer")]
    [DefaultValue(1)]
    public int UserId { get; set; }
}