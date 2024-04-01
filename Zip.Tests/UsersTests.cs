using System;
using System.Linq;
using System.Threading.Tasks;
using NSubstitute;
using Xunit;
using Zip.Tests.Extensions;
using Zip.WebAPI.Features.Users;
using Zip.WebAPI.Features.Users.Exceptions;
using Zip.WebAPI.Models;
using Zip.WebAPI.ServiceManager;

namespace Zip.Tests;

public class UsersTests
{
    private readonly IServiceManager _serviceManager = Substitute.For<IServiceManager>();
    
    [Fact]
    public async Task CreateUserCommand_Should_ReturnSuccess_WhenInputValid()
    {
        //Arrange
        var command = new CreateUserCommand{ Name = "test", Email = "test@test.com", Expenses = 100, Salary = 100};
        var handler = new CreateUserCommand.Handler(_serviceManager);
        
        _serviceManager.User.IsEmailUnique(command.Email).Returns(true);
        _serviceManager.User.CreateUser(Arg.Any<User>()).Returns(new User
            { Id = 1, Email = "test@test.com", Name = "test", Expenses = 100, Salary = 100 });
        
        //Act
        var result = await handler.Handle(command, default);
        
        //Assert
        Assert.IsAssignableFrom<UserDto>(result);
    }
    
    [Fact]
    public void CreateUserCommand_Should_ThrowValidationError_WhenNameNotSupplied()
    {
        //Arrange
        var command = new CreateUserCommand{ Email = "test@test.com", Expenses = 100, Salary = 100 };
        
        //Act
        var validation = ModelValidation.Validate(command);
        
        //Assert
        Assert.Contains(validation, validate => validate.MemberNames.Contains("Name"));
    }
    
    [Fact]
    public void CreateUserCommand_Should_ThrowValidationError_WhenEmailNotSupplied()
    {
        //Arrange
        var command = new CreateUserCommand{ Name = "test", Expenses = 100, Salary = 100 };
        
        //Act
        var validation = ModelValidation.Validate(command);
        
        //Assert
        Assert.Contains(validation, validate => validate.MemberNames.Contains("Email"));
    }
    
    [Fact]
    public void CreateUserCommand_Should_ThrowValidationError_WhenExpensesNotSupplied()
    {
        //Arrange
        var command = new CreateUserCommand{ Name = "test", Email = "test@test.com", Salary = 100 };
        
        //Act
        var validation = ModelValidation.Validate(command);
        
        //Assert
        Assert.Contains(validation, validate => validate.MemberNames.Contains("Expenses"));
    }
    
    [Fact]
    public void CreateUserCommand_Should_ThrowValidationError_WhenSalaryNotSupplied()
    {
        //Arrange
        var command = new CreateUserCommand{ Name = "test", Email = "test@test.com", Expenses = 100 };
        
        //Act
        var validation = ModelValidation.Validate(command);
        
        //Assert
        Assert.Contains(validation, validate => validate.MemberNames.Contains("Salary"));
    }
    
    [Fact]
    public void CreateUserCommand_Should_ThrowValidationError_WhenExpensesOutOfRange()
    {
        //Arrange
        var command = new CreateUserCommand{ Name = "test", Email = "test@test.com", Expenses = -1, Salary = 100 };
        
        //Act
        var validation = ModelValidation.Validate(command);
        
        //Assert
        Assert.Contains(validation, validate => validate.MemberNames.Contains("Expenses"));
    }
    
    [Fact]
    public void CreateUserCommand_Should_ThrowValidationError_WhenSalaryOutOfRange()
    {
        //Arrange
        var command = new CreateUserCommand{ Name = "test", Email = "test@test.com", Expenses = 100, Salary = -1 };
        
        //Act
        var validation = ModelValidation.Validate(command);
        
        //Assert
        Assert.Contains(validation, validate => validate.MemberNames.Contains("Salary"));
    }
    
    [Fact]
    public async Task CreateUserCommand_Should_ThrowUserNotFoundError_WhenEmailNotUnique()
    {
        //Arrange
        var command = new CreateUserCommand{ Name = "test", Email = "test@test.com", Expenses = 100, Salary = 100};
        var handler = new CreateUserCommand.Handler(_serviceManager);
        
        _serviceManager.User.IsEmailUnique(command.Email).Returns(false);
        
        //Act
        Func<Task> act = () => handler.Handle(command, default);
        
        //Assert
        await Assert.ThrowsAsync<EmailNotUniqueException>(act);
    }
    
}