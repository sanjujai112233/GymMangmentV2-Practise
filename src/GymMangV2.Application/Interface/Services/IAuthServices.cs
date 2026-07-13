using GymMangV2.Application.DTOs.Auth;
using GymMangV2.Application.Interfaces;

namespace GymMangV2.Application.Interfaces.Services;
public interface IAuthServices
{
    Task RegisterAsync(LoginRequest request);
    Task<LoginResponse> LoginAsync(LoginRequest request);
    Task<LoginResponse> RefreshTokenAsync(RefreshTokenRequest request);
    Task LogoutAsync(LogoutRequest request);
}