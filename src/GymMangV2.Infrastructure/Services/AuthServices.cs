using System.Reflection.Metadata.Ecma335;
using Azure.Core;
using GymMangV2.Application.DTOs.Auth;
using GymMangV2.Application.Interfaces.Security;
using GymMangV2.Application.Interfaces.Services;
using GymMangV2.Domain.Entities;
using GymMangV2.Infrastructure.DbBridge;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;

namespace GymMangV2.Infrastructure.Services;

public class AuthService : IAuthServices
{
    private readonly GymDbContext _dbContext;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtService _jwtService;
    private readonly ILogger<AuthService> _logger;

    public AuthService(
        GymDbContext dbContext,
        IPasswordHasher passwordHasher,
        IJwtService jwtService,
        ILogger<AuthService> logger)
    {
        _dbContext = dbContext;
        _passwordHasher = passwordHasher;
        _jwtService = jwtService;
        _logger = logger;
    }
    public async Task RegisterAsync(LoginRequest request)
    {
        var user = new User
        {
            UserName = request.Username,
            PassworHash = _passwordHasher.HashPassword(request.Password),
            RoleId = 3,
        };

        _dbContext.Users.Add(user);
        await _dbContext.SaveChangesAsync();
    }
    public async Task<LoginResponse> LoginAsync(LoginRequest request)
    {
        var user = await _dbContext.Users.Include(u => u.Role)
        .FirstOrDefaultAsync(u => u.UserName == request.Username);

        if (user is null)
        {
            _logger.LogInformation(
                "Login attempt for username {Username}",
                 request.Username
                );

            throw new Exception("Invalid username");
        }
        var isPasswordValid = _passwordHasher.verifyPassword(
            request.Password,
             user.PassworHash);

        if (!isPasswordValid){
            _logger.LogInformation(
                "Login attempt for username {Username}",
                 request.Username
                );
            throw new Exception("Invalid username or password");
            }

        var accessToken = _jwtService.GeneratingToken(
            user.Id,
            user.UserName,
            user.Role.Name
        );
        var refreshToken = new RefreshToken
        {
            Token = Guid.NewGuid().ToString(),
            UserId = user.Id,
            ExpirestAt = DateTime.UtcNow.AddDays(7),
            IsRevoked = false
        };
        _dbContext.RefreshTokens.Add(refreshToken);
        await _dbContext.SaveChangesAsync();
        _logger.LogInformation(
                "user {Username} logged in successfully",
                 request.Username
                );

        return new LoginResponse
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken.Token
        };


    }

    public async Task<LoginResponse> RefreshTokenAsync(RefreshTokenRequest request)
    {
        var refreshToken = await _dbContext.RefreshTokens
        .Include(x => x.user)
        .ThenInclude(u => u.Role)
        .FirstOrDefaultAsync(x => x.Token == request.RefreshToken);

        if (refreshToken is null)
            throw new Exception("Invalid refresh token");

        if (refreshToken.IsRevoked)
            throw new Exception("Refresh token revoked");

        if (refreshToken.ExpirestAt <= DateTime.UtcNow)
            throw new Exception("Refresh Tokken expired");

        var accessToken = _jwtService.GeneratingToken(
            refreshToken.user.Id,
            refreshToken.user.UserName,
            refreshToken.user.Role.Name
        );

        //Now we Token rotation (old -> New) Revoke
        refreshToken.IsRevoked = true;
        refreshToken.RevokedAt = DateTime.UtcNow;

        var newRefreshToken = new RefreshToken
        {
            Token = Guid.NewGuid().ToString(),
            UserId = refreshToken.user.Id,
            ExpirestAt = DateTime.UtcNow.AddDays(7),
            IsRevoked = false
        };
        _dbContext.RefreshTokens.Add(newRefreshToken);
        await _dbContext.SaveChangesAsync();

        return new LoginResponse
        {
            AccessToken = accessToken,
            RefreshToken = newRefreshToken.Token
        };
    }

    public async Task LogoutAsync(LogoutRequest request)
    {
        var refreshToken = await _dbContext.RefreshTokens.FirstOrDefaultAsync(x => x.Token == request.RefreshToken);
        if (refreshToken is null)
            throw new Exception("Invalid refresh token");

        if (refreshToken.IsRevoked)
            return;

        refreshToken.IsRevoked = true;
        refreshToken.RevokedAt = DateTime.UtcNow;

        await _dbContext.SaveChangesAsync();

    }


}