using GymMangV2.Application.Interfaces;
using GymMangV2.Domain.Entities;
using GymMangV2.Infrastructure.DbBridge;
using Microsoft.EntityFrameworkCore;

namespace GymMangV2.Infrastructure.Repositories;

public class MemberRepository : IMemberRepository
{
    private readonly GymDbContext _context;
    public MemberRepository(GymDbContext context)
    {
        _context = context;
    }

    public async Task<Member?> GetByIdAsync(int id)
    {
        return await _context.Members.Include(x=>x.Trainer)
        .FirstOrDefaultAsync(x=>x.Id == id);
        
    }
    public async Task< List<Member>> GetAllAsync()
    {
        return await _context.Members.Include(x=> x.Trainer).ToListAsync();
    }
    public async Task AddAsync(Member member)
    {
        await _context.Members.AddAsync(member);
        
    }
    public async Task UpdateAsync(Member member)
    {
        _context.Members.Update(member);
        return;
    }
    public Task DeleteAsync(Member member)
    {
         _context.Members.Remove(member);
         return Task.CompletedTask;
        
    }
    public async Task<bool> EmailExistAsync(string Email)
    {
        return await _context.Members.AnyAsync(u=> u.Email == Email);
    }
    public async Task<bool> PhoneExistAsync(string phone)
    {
        return await _context.Members.AnyAsync(u=>u.Phone == phone);
    }
    public async Task SaveChangeAsync()
    {
        await _context.SaveChangesAsync();
        //return Task.CompletedTask;
    }
    
}