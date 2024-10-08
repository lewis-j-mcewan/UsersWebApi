using Zip.WebAPI.Features.Accounts;
using Zip.WebAPI.Features.Users;

namespace Zip.WebAPI.ServiceManager;

public interface IServiceManager
{
    IUserService User { get; }
    IAccountService Account { get; }
}