using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Zip.WebAPI.Features.Accounts.Exceptions;
using Zip.WebAPI.Models;
using Zip.WebAPI.ServiceManager;

namespace Zip.WebAPI.Features.Accounts;

public class GetAccountsForUserQueryHandler : IRequestHandler<GetAccountsForUserQuery, IEnumerable<AccountDto>>
{
    private readonly IServiceManager _serviceManager;

    public GetAccountsForUserQueryHandler(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }
    
    public async Task<IEnumerable<AccountDto>> Handle(GetAccountsForUserQuery request, CancellationToken cancellationToken)
    {
        var user = _serviceManager.User.GetUser(request.UserId).Result;

        if (user is null)
        {
            throw new UserNotFoundException(request.UserId);
        }
        
        var result = await _serviceManager.Account.GetAccounts(request.UserId);

        return result.Select(account => new AccountDto
        {
            Id = account.Id, 
            UserId = account.UserId, 
            Description = account.Description
        });
    }
}
