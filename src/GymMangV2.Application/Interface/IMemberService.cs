using GymMangV2.Application.DTOs.Members;

namespace GymMangV2.Application.Interfaces;

public interface IMemberService
{
    Task<MemberResponseDto> CreateAsync(CreateMemberRequestDto request);
    Task<List<MemberResponseDto>> GetAllAsync();
    Task<MemberResponseDto?> GetByIdAsync(int id);
    Task UpdateAsync(int id, UpdateMemberRequestDto request);
    Task DeleteAsync(int id);
}