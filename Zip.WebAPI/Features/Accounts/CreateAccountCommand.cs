using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Zip.WebAPI.Models;
using Zip.WebAPI.ServiceManager;

namespace Zip.WebAPI.Features.Accounts;

public class CreateAccountCommand : IRequest<AccountDto>
{
    public int UserId { get; set; }
    public string Description { get; set; }
    
    public class Handler : IRequestHandler<CreateAccountCommand, AccountDto>
    {
        private IServiceManager _serviceManager;
        
        public Handler(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }
        
        public async Task<AccountDto> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {
            var user = _serviceManager.User.GetUser(request.UserId).Result;
            Console.WriteLine("1");
            if (user is null)
            {
                throw new Exception();
            }

            if (user.Salary - user.Expenses < 1000)
            {
                throw new Exception();
            }
            Console.WriteLine("2");
            var account = new Account
            {
                UserId = request.UserId,
                Description = request.Description
            };
            Console.WriteLine("3");
            var result = await _serviceManager.Account.CreateAccount(account);
            Console.WriteLine("4");
            var accountDto = new AccountDto
            {
                Id = result.Id,
                UserId = result.UserId,
                Description = result.Description
            };
            Console.WriteLine("5");
            return accountDto;
        }
    }
}