using GymMangV2.Domain.Common;

namespace GymMangV2.Domain.Entities;

public class Attendance : BaseEntity
{
    public int MemberID { get; set; }
    public Member Member { get; set; }=  null;
    public DateTime CheckInTime { get; set; }
    public DateTime CheckOutTime { get; set; }
}