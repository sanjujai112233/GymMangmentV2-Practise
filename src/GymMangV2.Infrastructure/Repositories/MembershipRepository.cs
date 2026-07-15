using GymMangV2.Application.Interfaces;
using GymMangV2.Domain.Entities;
using GymMangV2.Infrastructure.DbBridge;
using Microsoft.EntityFrameworkCore;

namespace GymMangV2.Infrastructure.Repositories;

public class MembershipRepository : IMembershipRepository
{
    private readonly GymDbContext _context; 
    public MembershipRepository(GymDbContext context)
    {
        _context = context;
    }

   
    public async Task<List<MemberShip>> GetExpiringMembershipsAsync()
    {
        var tomorrow = DateTime.UtcNow.Date.AddDays(1);

        return await _context.MemberShips
            .Include(x => x.Member)
            .Where(x => x.EndDate.Date == tomorrow)
            .ToListAsync();
    }
    
}