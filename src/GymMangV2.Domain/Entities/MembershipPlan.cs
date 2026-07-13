using GymMangV2.Domain.Common;

namespace GymMangV2.Domain.Entities;

public class MembershipPlan : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int DurationInDays { get; set; }
    public string Description { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
    public ICollection<MemberShip> Membership { get; set; } = new List<MemberShip>();
}