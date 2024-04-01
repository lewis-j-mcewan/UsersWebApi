using System.Linq;
using Zip.WebAPI.Data;
using Zip.WebAPI.Models;

namespace Zip.WebAPI.SeedData;

public static class SeedData
{
    public static void Seed(ZipPayContext context)
    {
        if (!context.Users.Any())
        {
            var users = new User[]
            {
                new() { Name = "Nigel", Email = "nigel@nomates.com", Salary = 20000, Expenses = 1000 },
                new() { Name = "Harry", Email = "harry@thehippo.com", Salary = 1200, Expenses = 1000 },
                new() { Name = "Roger", Email = "roger@oldmate.com", Salary = 1200, Expenses = 0 },
                new() { Name = "Johnny", Email = "johnny@boy.com", Salary = 4000, Expenses = 5000 }
            };
        
            context.Users.AddRange(users);
        }

        context.SaveChanges();

        if (!context.Accounts.Any())
        {
            var accounts = new Account[]
            {
                new() { UserId = 1, Description = "major key" },
                new() { UserId = 3, Description = "another one" }
            };
        
            context.Accounts.AddRange(accounts);
        }

        context.SaveChanges();
    }
}