using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Zip.WebAPI.Models;

namespace Zip.WebAPI.Features.Accounts;

[Route("api/[controller]")]
[ApiController]
public class AccountsController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public AccountsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [ProducesResponseType(typeof(AccountDto), StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateAccountAsync([FromBody] CreateAccountCommand body)
    {
        try
        {
            var result = await _mediator.Send(body);
            string location =  "/" + ControllerContext.ActionDescriptor.ControllerName + "/" + result.Id;
            return Created(location, result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }
    }
}