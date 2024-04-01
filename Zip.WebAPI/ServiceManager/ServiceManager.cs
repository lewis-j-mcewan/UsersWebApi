using Zip.WebAPI.Data;
using Zip.WebAPI.Features.Users;

namespace Zip.WebAPI.ServiceManager;

public class ServiceManager : IServiceManager
{
    private readonly ZipPayContext _context;
    private IUserService _userService;

    public ServiceManager(ZipPayContext context)
    {
        _context = context;
    }

    public IUserService User
    {
        get
        {
            if (_userService == null)
            {
                _userService = new UserService(_context);
            }

            return _userService;
        }
    }
}