using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Zip.WebAPI.Models;

namespace Zip.WebAPI.Features.Users
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateUserAsync([FromBody] CreateUserCommand body)
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
}
