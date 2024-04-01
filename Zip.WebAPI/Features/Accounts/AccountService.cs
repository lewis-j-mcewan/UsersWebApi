using System.Threading.Tasks;
using Zip.WebAPI.Data;
using Zip.WebAPI.Models;

namespace Zip.WebAPI.Features.Accounts;

public class AccountService : IAccountService
{
    private readonly ZipPayContext _context;

    public AccountService(ZipPayContext context)
    {
        _context = context;
    }
    
    public async Task<Account> CreateAccount(Account account)
    {
        await _context.Accounts.AddAsync(account);
        await _context.SaveChangesAsync();
        return account;
    }
}