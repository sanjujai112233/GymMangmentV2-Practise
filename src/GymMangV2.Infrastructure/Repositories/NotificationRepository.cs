using GymMangV2.Application.Interfaces;
using GymMangV2.Domain.Entities;
using GymMangV2.Domain.Enums;
using GymMangV2.Infrastructure.DbBridge;
using Microsoft.EntityFrameworkCore;

namespace GymMangV2.Infrastructure.Repositories;

public class NotificationRepository : INotificationRepository
{
    private readonly GymDbContext _context;
    public NotificationRepository(GymDbContext context)
    {
        _context = context;
    }


    public async Task<List<NotificationQueue>> GetPendingNotificationsAsync(int batchSize)
    {
        return await _context.NotificationQueues
        .Where(x=> x.status == NotificationStatus.Pending)
        .OrderBy(x=>x.CreatedOn)
        .Take(batchSize)
        .ToListAsync();
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    
}