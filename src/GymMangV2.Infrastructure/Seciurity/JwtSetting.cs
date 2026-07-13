namespace GymMangV2.Infrastructure.Security;

public class JwtSetting
{
    public string SecretKey { get; set; } = string.Empty;
    public string Issuer { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;

    public int ExpiryInMinutes { get; set; } = 60;

    public int ExpiryIntMinutes
    {
        get => ExpiryInMinutes;
        set => ExpiryInMinutes = value;
    }
}