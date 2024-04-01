using System.Collections.Generic;
using System.Threading.Tasks;
using Zip.WebAPI.Models;

namespace Zip.WebAPI.Features.Accounts;

public interface IAccountService
{
    Task<IEnumerable<Account>> GetAccounts(int userId);
    Task<Account> CreateAccount(Account account);
}