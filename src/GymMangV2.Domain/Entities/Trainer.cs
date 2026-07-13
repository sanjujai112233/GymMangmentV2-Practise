using GymMangV2.Domain.Common;

namespace GymMangV2.Domain.Entities;
public class Trainer : BaseEntity
{
    public string FullName { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Specialization { get; set; } = string.Empty;
    public int Experience { get; set; }
    public decimal Salary { get; set; }

    public ICollection<Member> Members = new List<Member>();

    
}