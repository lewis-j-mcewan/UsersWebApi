using System;

namespace Zip.WebAPI.Features.Accounts.Exceptions;

public class UserNotFoundException : Exception
{
    public UserNotFoundException(int userId) : base($"User with id: {userId} could not be found")
    {
    }
}