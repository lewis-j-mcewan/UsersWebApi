using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Zip.WebAPI.Features.Accounts.Exceptions;
using Zip.WebAPI.Models;

namespace Zip.WebAPI.Features.Accounts;

[Route("api/[controller]")]
[ApiController]
public class AccountsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger _logger;
    
    public AccountsController(IMediator mediator, ILogger<AccountsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<AccountDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAccountsForUserAsync([FromQuery] GetAccountsForUserQuery query)
    {
        try
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        catch (UserNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError("Could not return accounts for specified user: {error}", ex);
            return BadRequest("Could not return accounts for user");
        }
    }

    [HttpPost]
    [ProducesResponseType(typeof(AccountDto), StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateAccountAsync([FromBody] CreateAccountCommand body)
    {
        try
        {
            var result = await _mediator.Send(body);
            string location = "/" + ControllerContext.ActionDescriptor.ControllerName + "/" + result.Id;
            return Created(location, result);
        }
        catch (UserNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (InsufficientIncomeException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError("The account could not be created for the user: {error}", ex);
            return BadRequest("The account could not be created");
        }
    }
}