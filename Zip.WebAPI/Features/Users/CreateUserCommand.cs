using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Zip.WebAPI.Features.Users.Exceptions;
using Zip.WebAPI.Models;
using Zip.WebAPI.ServiceManager;

namespace Zip.WebAPI.Features.Users;

public class CreateUserCommand : IRequest<UserDto>
{
    [Required]
    public string Name { get; set; }
    [Required]
    public string Email { get; set; }
    [Required, Range(0, int.MaxValue, ErrorMessage = "Expenses must be a positive number")]
    public decimal? Expenses { get; set; }
    [Required, Range(0, int.MaxValue, ErrorMessage = "Salary must be a positive number")]
    public decimal? Salary { get; set; }
    
    public class Handler : IRequestHandler<CreateUserCommand, UserDto>
    {
        private readonly IServiceManager _serviceManager;

        public Handler(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }
        
        public async Task<UserDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = new User
            {
                Name = request.Name,
                Email = request.Email,
                Expenses = request.Expenses,
                Salary = request.Salary
            };
            
            var uniqueEmail = _serviceManager.User.IsEmailUnique(user.Email).Result;
            if (!uniqueEmail)
                throw new EmailNotUniqueException();
            
            var result = await _serviceManager.User.CreateUser(user);
            
            var userDto = new UserDto
            {
                Id = result.Id,
                Name = result.Name,
                Email = result.Email,
                Expenses = result.Expenses,
                Salary = result.Salary
            };
            
            return userDto;
        }
    }
    
}