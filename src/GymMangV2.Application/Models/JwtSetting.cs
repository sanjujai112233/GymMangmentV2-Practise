namespace GymMangV2.Application.Models
{
    public class JwtSetting
    {
        // Section name used in appsettings.json (currently "Jwt")
        public const string SectionName = "Jwt";

        public string SecretKey { get; set; } = string.Empty;
        public string Issuer { get; set; } = string.Empty;
        public string Audience { get; set; } = string.Empty;

        // Friendly alias used by appsettings.json
        public int ExpiryInMinutes { get; set; } = 60;

        // Backward-compatible alias for existing code
        public int ExpiryIntMinutes
        {
            get => ExpiryInMinutes;
            set => ExpiryInMinutes = value;
        }
    }
}
