using Microsoft.Extensions.Logging;
using FluentAssertions;
using GymMangV2.Application.DTOs.Auth;
using GymMangV2.Application.Exceptions;
using GymMangV2.Application.Interfaces.Security;
using GymMangV2.Domain.Entities;
using GymMangV2.Infrastructure.Services;
using Moq;

public class AuthServiceTests
{
    //private readonly Mock<> _userRepo;
    // private readonly Mock<IJwtService> _jwtService;
    // private readonly Mock<IPasswordHasher> _passwordHasher;

    // private readonly Mock<ILogger<AuthService>> _logger;

    public AuthServiceTests()
    {
        // _userRepo = new Mock<IUserRepository>();

        // _jwtService = new Mock<IJwtService>();

        // _passwordHasher = new Mock<IPasswordHasher>();
        // _logger = new Mock<ILogger<AuthService>>();

        // _service = new AuthService(
        //     _userRepo.Object,
        //     _passwordHasher.Object,
        //     _jwtService.Object,
        //     _logger.Object
        //     );
    }

    // [Fact]
    // public async Task LoginAsync_ShouldReturnToken_WhenCredentialsAreValid()
    // {
    //     // Arrange

    //     var user = new User()
    //     {
    //         Id = 1,
    //         UserName = "Admin",
    //         RoleId = 1

    //     };

    //     // _userRepo
    //     //     .Setup(x => x.GetByEmailAsync(It.IsAny<string>()))
    //     //     .ReturnsAsync(user);

    //     _passwordHasher
    //         .Setup(x => x.verifyPassword(
    //             It.IsAny<string>(),
    //             It.IsAny<string>()))
    //         .Returns(true);

    //     _jwtService
    //         .Setup(x => x.GeneratingToken(user.Id, user.UserName, "Admin"))
    //         .Returns("JWT_TOKEN");

    //     // Act

    //     var result = await _service.LoginAsync(
    //         new LoginRequest());

    //     // Assert

    //     result.Should().NotBeNull();

    //     result.AccessToken.Should().Be("JWT_TOKEN");

    //     _jwtService.Verify(
    //         x => x.GeneratingToken(user.Id, user.UserName, "Admin"),
    //         Times.Once);
    // }
    // [Fact]
    // public async Task LoginAsync_ShouldThrowException_WhenPasswordIsWrong()
    // {
    //     // Arrange

    //     var user = new User()
    //     {
    //         Id = 1,
    //         UserName = "Admin",
    //         RoleId = 1

    //     };


    //     // _userRepo
    //     //     .Setup(x => x.GetByEmailAsync(It.IsAny<string>()))
    //     //     .ReturnsAsync(user);

    //     _passwordHasher
    //         .Setup(x => x.verifyPassword(
    //             It.IsAny<string>(),
    //             It.IsAny<string>()))
    //         .Returns(false);

    //     // Act

    //     Func<Task> action =
    //         () => _service.LoginAsync(new LoginRequest());

    //     // Assert

    //     await action.Should()
    //         .ThrowAsync<BusinessException>();

    //     // _jwtService.Verify(
    //     //     x => x.GeneratingToken(It.IsAny <user> ()),
    //     //     Times.Never);
    // }
}