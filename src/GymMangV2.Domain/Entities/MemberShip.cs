using GymMangV2.Domain.Common;
using GymMangV2.Domain.Enums;

namespace GymMangV2.Domain.Entities;

public class MemberShip : BaseEntity
{
    public int MemberID { get; set; }
    public Member Member { get; set; } = null;
    public int MemberShipPlanId { get; set; }
    public MembershipPlan MembershipPlan { get; set; } = null;

    public DateTime StartingDate { get; set; }
    public DateTime EndDate { get; set; }
    public MembershipStatus IsActive { get; set; }
    public ICollection<Payment> Payments { get; set; } = new List<Payment>();
}