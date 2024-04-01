using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Zip.WebAPI.Features.Users.Exceptions;
using Zip.WebAPI.Models;

namespace Zip.WebAPI.Features.Users;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger _logger;

    public UsersController(IMediator mediator, ILogger<UsersController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpPost]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateUserAsync([FromBody] CreateUserCommand body)
    {
        try
        {
            var result = await _mediator.Send(body);
            string location = "/" + ControllerContext.ActionDescriptor.ControllerName + "/" + result.Id;
            return Created(location, result);
        }
        catch (EmailNotUniqueException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError("Failed to create a new user. Error: {error}", ex);
            return BadRequest(ex.Message);
        }
    }
}
