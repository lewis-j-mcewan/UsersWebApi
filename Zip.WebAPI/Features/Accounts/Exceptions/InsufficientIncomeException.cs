using System;

namespace Zip.WebAPI.Features.Accounts.Exceptions;

public class InsufficientIncomeException : Exception
{
    public InsufficientIncomeException() : base("The users' net income is too low to create an account")
    {
    }
}