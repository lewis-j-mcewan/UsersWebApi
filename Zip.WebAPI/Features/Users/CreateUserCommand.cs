using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Zip.WebAPI.Models;
using Zip.WebAPI.ServiceManager;

namespace Zip.WebAPI.Features.Users;

public class CreateUserCommand : IRequest<UserDto>
{
    public string Name { get; set; }
    public string Email { get; set; }
    public decimal Expenses { get; set; }
    public decimal Salary { get; set; }
    
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