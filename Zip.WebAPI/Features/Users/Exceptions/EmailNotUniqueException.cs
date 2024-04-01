using System;

namespace Zip.WebAPI.Features.Users.Exceptions;

public class EmailNotUniqueException : Exception
{
    public EmailNotUniqueException() : base("The provided email already exists"){}
    
}