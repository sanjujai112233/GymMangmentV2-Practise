namespace GymMangV2.Application.Interfaces.Security;
public interface IPasswordHasher
{
    string HashPassword(string password);
    bool verifyPassword(string password, string passwordHash);
    
}