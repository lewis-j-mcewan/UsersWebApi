using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Zip.WebAPI.Features.Accounts.Exceptions;
using Zip.WebAPI.Models;
using Zip.WebAPI.ServiceManager;

namespace Zip.WebAPI.Features.Accounts;

public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, AccountDto>
{
        private readonly IServiceManager _serviceManager;
        
        public CreateAccountCommandHandler(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }
        
        public async Task<AccountDto> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {
            var user = _serviceManager.User.GetUser(request.UserId).Result;
            
            if (user is null)
            {
                throw new UserNotFoundException(request.UserId);
            }
            
            if (user.Salary - user.Expenses < 1000)
            {
                throw new InsufficientIncomeException();
            }
            
            var account = new Account
            {
                UserId = request.UserId,
                Description = request.Description
            };
            
            var result = await _serviceManager.Account.CreateAccount(account);
            
            var accountDto = new AccountDto
            {
                Id = result.Id,
                UserId = result.UserId,
                Description = result.Description
            };
            
            return accountDto;
        }
}