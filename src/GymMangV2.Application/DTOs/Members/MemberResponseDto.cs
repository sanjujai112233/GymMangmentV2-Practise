namespace GymMangV2.Application.DTOs.Members;

public class MemberResponseDto
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string TrainerName { get; set; } = string.Empty;
}