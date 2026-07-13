namespace GymMangV2.Application.Interfaces.Security;

public interface IJwtService
{
    string GeneratingToken(int userId, string username, string role);
}