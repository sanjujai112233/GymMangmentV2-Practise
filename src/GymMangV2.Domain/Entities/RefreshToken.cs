using GymMangV2.Domain.Common;

namespace GymMangV2.Domain.Entities;

public class RefreshToken : BaseEntity
{
    public string Token { get; set; } =string.Empty;
    public DateTime ExpirestAt { get; set; }
    public bool IsRevoked { get; set; }
    public DateTime?  RevokedAt { get; set; }
    public int UserId  { get; set; }
    public User user { get; set; } = null;
}