using GymMangV2.Domain.Common;
using GymMangV2.Domain.Enums;

namespace GymMangV2.Domain.Entities;

public class NotificationQueue :BaseEntity
{
    public int MemberId { get; set; }
    public Member Member { get; set; } = null;
    public string Phone { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public NotificationStatus status  { get; set; } 
    public int  RetryCount { get; set; }
    public DateTime? ProcessdOn { get; set; }
}