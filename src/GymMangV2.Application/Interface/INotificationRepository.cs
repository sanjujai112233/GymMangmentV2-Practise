using GymMangV2.Domain.Entities;

namespace GymMangV2.Application.Interfaces;

public interface INotificationRepository
{
    Task<List<NotificationQueue>> GetPendingNotificationsAsync(int bachSize);
    Task SaveChangesAsync();
}