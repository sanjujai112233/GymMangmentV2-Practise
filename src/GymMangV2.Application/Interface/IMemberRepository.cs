using GymMangV2.Domain.Entities;

namespace GymMangV2.Application.Interfaces;

public interface IMemberRepository
{
    Task<Member> GetByIdAsync(int id);
    Task<List<Member>> GetAllAsync();
    Task AddAsync(Member member);
    Task UpdateAsync(Member member);
    Task DeleteAsync(Member member);
    Task<bool> EmailExistAsync(string email);
    Task<bool> PhoneExistAsync(string phone);
}
