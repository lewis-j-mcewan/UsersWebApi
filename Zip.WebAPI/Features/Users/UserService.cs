using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Zip.WebAPI.Data;
using Zip.WebAPI.Models;

namespace Zip.WebAPI.Features.Users;

public class UserService : IUserService
{

    private readonly ZipPayContext _context;

    public UserService(ZipPayContext context)
    {
        _context = context;
    }

    public async Task<User> GetUser(int userId)
    {
        var user = await _context.Users.SingleOrDefaultAsync(user => user.Id == userId);
        return user;
    }

    public async Task<bool> IsEmailUnique(string email)
    {
        var result = await _context.Users
            .SingleOrDefaultAsync(user => user.Email.Equals(email));
        return result is null;
    }

    public async Task<User> CreateUser(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        return user;
    }
}