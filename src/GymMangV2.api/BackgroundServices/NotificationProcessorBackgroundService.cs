using GymMangV2.Application.Interfaces;
using GymMangV2.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace GymMangV2.api.BackgroundServices;

public class NotificationProcessorBackgroundService : BackgroundService
{
    private readonly ILogger<NotificationProcessorBackgroundService> _logger;
    private readonly IServiceScopeFactory _scopeFactory;

    public NotificationProcessorBackgroundService(
        ILogger<NotificationProcessorBackgroundService> logger,
        IServiceScopeFactory scopeFactory
    )
    {
        _scopeFactory = scopeFactory;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Notification Process started");
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                using var scope = _scopeFactory.CreateScope();

                var NotificationRepo = scope.ServiceProvider
                .GetRequiredService<INotificationRepository>();

                var notifications = await NotificationRepo
                .GetPendingNotificationsAsync(100);

                foreach (var noti in notifications)
                {
                    try
                    {
                        // Whatever the work we have to do. 
                        //Send message in whatsapp or Messages



                        _logger.LogInformation(
                            "Sending notification to {Phone}",
                            noti.Phone);

                        await Task.Delay(500, stoppingToken);

                        noti.status = NotificationStatus.Sent;
                        noti.ProcessdOn = DateTime.UtcNow;

                        _logger.LogInformation(
                            "Notification sent successfully to {Phone}",
                            noti.Phone);
                    }
                    catch (Exception ex)
                    {
                        noti.RetryCount++;
                        if (noti.RetryCount >= 3)
                        {
                            noti.status = NotificationStatus.Failed;
                            _logger.LogError(ex,
                                "Notification permanently failed for {Phone}",
                                noti.Phone);
                        }
                        else
                        {
                            noti.status = NotificationStatus.Pending;
                            _logger.LogWarning("Notification failed. Retry {Retry}",
                            noti.RetryCount);
                        }
                    }

                }
                await NotificationRepo.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "Error while processing notification queue.");
            }
            await Task.Delay(TimeSpan.FromSeconds(30), stoppingToken);
        }
        _logger.LogInformation("Notification Processor Stopped.");
    }

}