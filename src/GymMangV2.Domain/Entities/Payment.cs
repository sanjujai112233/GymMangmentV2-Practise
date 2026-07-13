using GymMangV2.Domain.Common;
using GymMangV2.Domain.Enums;

namespace GymMangV2.Domain.Entities;

public class Payment : BaseEntity
{
    public int MemberShipId { get; set; }
    public MemberShip MemberShip { get; set; } = null;
    public decimal Amount { get; set; }
    public DateTime PaymentDate { get; set; }
    public PaymentStatus Status { get; set; }
    
}