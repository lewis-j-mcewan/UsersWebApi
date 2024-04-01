using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Xunit;
using Zip.Tests.Extensions;
using Zip.WebAPI.Features.Accounts;
using Zip.WebAPI.Features.Accounts.Exceptions;
using Zip.WebAPI.Models;
using Zip.WebAPI.ServiceManager;

namespace Zip.Tests;

public class AccountsTests
{
    private readonly IServiceManager _serviceManager = Substitute.For<IServiceManager>();

    [Fact]
    public async Task GetAccountsForUserQuery_Should_ReturnSuccess_WhenUserIsFound()
    {
        //Arrange
        var command = new GetAccountsForUserQuery{ UserId = 1 };
        var handler = new GetAccountsForUserQuery.Handler(_serviceManager);
        
        _serviceManager.User.GetUser(command.UserId).Returns(new User
            { Id = 1, Email = "test@test.com", Name = "test", Expenses = 0, Salary = 1000 });
        
        _serviceManager.Account.GetAccounts(command.UserId).Returns(new Account[]
        {
            new Account{ Id = 1, UserId = 1, Description = "test1"},
            new Account{ Id = 2, UserId = 1, Description = "test2"},
        });
        
        //Act
        var result = await handler.Handle(command, default);
        
        //Assert
        Assert.IsAssignableFrom<IEnumerable<AccountDto>>(result);
        Assert.Equal(2, result.Count());
    }
    
    [Fact]
    public void GetAccountsForUserQuery_Should_ThrowValidationError_WhenInvalidUserIdSupplied()
    {
        //Arrange
        var command = new GetAccountsForUserQuery{ UserId = -1 };
        
        //Act
        var validation = ModelValidation.Validate(command);
        
        //Assert
        Assert.Contains(validation, validate => validate.MemberNames.Contains("UserId"));
    }
    
    [Fact]
    public async Task GetAccountsForUserQuery_Should_ThrowError_WhenUserNotFound()
    {
        //Arrange
        var command = new GetAccountsForUserQuery{ UserId = 1 };
        var handler = new GetAccountsForUserQuery.Handler(_serviceManager);
        _serviceManager.User.GetUser(command.UserId).ReturnsNull();
        
        //Act
        Func<Task> act = () => handler.Handle(command, default);
        
        //Assert
        await Assert.ThrowsAsync<UserNotFoundException>(act);
    }
    
    [Fact]
    public async Task CreateAccountCommand_Should_ReturnSuccess_WhenInputIsValid()
    {
        //Arrange
        var command = new CreateAccountCommand { UserId = 1, Description = "test1" };
        var handler = new CreateAccountCommand.Handler(_serviceManager);
        
        _serviceManager.User.GetUser(command.UserId).Returns(new User
            { Id = 1, Email = "test@test.com", Name = "test", Expenses = 0, Salary = 1000 });

        _serviceManager.Account.CreateAccount(Arg.Any<Account>())
            .Returns(new Account { Id = 1, UserId = 1, Description = "test1" });
        
        //Act
        var result = await handler.Handle(command, default);
        
        //Assert
        Assert.IsAssignableFrom<AccountDto>(result);
    }
    
    [Fact]
    public void CreateAccountCommand_Should_ThrowValidationError_WhenInvalidUserIdSupplied()
    {
        //Arrange
        var command = new CreateAccountCommand(){ UserId = -1 };
        
        //Act
        var validation = ModelValidation.Validate(command);
        
        //Assert
        Assert.Contains(validation, validate => validate.MemberNames.Contains("UserId"));
    }
    
    [Fact]
    public void CreateAccountCommand_Should_ThrowValidationError_WhenUserIdNotSupplied()
    {
        //Arrange
        var command = new CreateAccountCommand(){ Description = "test"};
        
        //Act
        var validation = ModelValidation.Validate(command);
        
        //Assert
        Assert.Contains(validation, validate => validate.MemberNames.Contains("UserId"));
    }

    [Fact]
    public async Task CreateAccountCommand_Should_ThrowError_WhenUserNotFound()
    {
        //Arrange
        var command = new CreateAccountCommand
        {
            UserId = 1,
            Description = "test1"
        };
        var handler = new CreateAccountCommand.Handler(_serviceManager);
        _serviceManager.User.GetUser(command.UserId).ReturnsNull();
        
        //Act
        Func<Task> act = () => handler.Handle(command, default);
        
        //Assert
        await Assert.ThrowsAsync<UserNotFoundException>(act);
    }

    [Fact]
    public async Task CreateAccountCommand_Should_ThrowError_WhenSalaryMinusExpensesLessThan1000()
    {
        //Arrange
        var command = new CreateAccountCommand
        {
            UserId = 1,
            Description = "test1"
        };
        var handler = new CreateAccountCommand.Handler(_serviceManager);
        _serviceManager.User.GetUser(command.UserId).Returns(new User
            { Id = 1, Email = "test@test.com", Name = "test", Expenses = 1, Salary = 1000 });
        
        //Act
        Func<Task> act = () => handler.Handle(command, default);
        
        //Assert
        await Assert.ThrowsAsync<InsufficientIncomeException>(act);
    }
}