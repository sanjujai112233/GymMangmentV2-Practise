using GymMangV2.Infrastructure.Security;
using Microsoft.Extensions.Options;

var settings = new JwtSetting
{
    SecretKey = "THIS_IS_A_LONG_SECRET_KEY_AT_LEAST_32_CHARACTERS_LONG",
    Issuer = "GymMangV2",
    Audience = "GymMangV2Users",
    ExpiryInMinutes = 60
};

var service = new JwtService(Options.Create(settings));
Console.WriteLine(service.GeneratingToken(1, "demo", "Admin"));
