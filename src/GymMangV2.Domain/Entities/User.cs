using GymMangV2.Domain.Common;

namespace GymMangV2.Domain.Entities;
public class User : BaseEntity
{
    public string UserName { get; set; } = string.Empty;
    public string PassworHash { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
    //FK
    public int RoleId { get; set; }  
    //Navigation Property
    public Role Role { get; set; } = null;
    public Member? Member { get; set; }
    public ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
}