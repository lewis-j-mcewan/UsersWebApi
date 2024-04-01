using System.Threading.Tasks;
using Zip.WebAPI.Models;

namespace Zip.WebAPI.Features.Accounts;

public interface IAccountService
{
    Task<Account> CreateAccount(Account account);
}