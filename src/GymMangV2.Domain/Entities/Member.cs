using GymMangV2.Domain.Common;
using GymMangV2.Domain.Enums;

namespace GymMangV2.Domain.Entities;

public class Member : BaseEntity
{
    public string FullName { get; set; } = string.Empty;
    public DateTime Dob { get; set; } 
    public Gender Gender { get; set; } 
    public string Phone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public decimal Height { get; set; } 
    public decimal Weight { get; set; } 
    public DateTime JoiningDate { get; set; }

    public int UserId { get; set; }
    public User User { get; set; } = null;
    public int? TrainerId { get; set; }
    public Trainer? Trainer { get; set; }

    public ICollection<MemberShip> MemberShips { get; set; } = new List<MemberShip>();
    public ICollection<Attendance> Attendences { get; set; } = new List<Attendance>();
}
