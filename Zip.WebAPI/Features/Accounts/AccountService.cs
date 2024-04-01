using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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

    public async Task<IEnumerable<Account>> GetAccounts(int userId)
    {
        return await _context.Accounts
            .Where(user => user.UserId == userId)
            .ToListAsync();
    }

    public async Task<Account> CreateAccount(Account account)
    {
        await _context.Accounts.AddAsync(account);
        await _context.SaveChangesAsync();
        return account;
    }
}