using System.Threading.Tasks;
using Zip.WebAPI.Models;

namespace Zip.WebAPI.Features.Users;

public interface IUserService
{
    Task<User> GetUser(int userId);
    Task<User> CreateUser(User user);
}