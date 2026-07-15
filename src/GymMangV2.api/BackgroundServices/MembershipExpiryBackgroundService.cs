using GymMangV2.Infrastructure.Interfaces;

namespace GymMangV2.api.BackgroundServices;

public class MembershipExpiryBackgroundService : BackgroundService
{
    private readonly ILogger<MembershipExpiryBackgroundService> _logger;
    private readonly IServiceScopeFactory _scopeFactory;
    public MembershipExpiryBackgroundService(
        ILogger<MembershipExpiryBackgroundService> logger,
        IServiceScopeFactory scopeFactory
    )
    {
        _logger = logger;
        _scopeFactory = scopeFactory;
    }


    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("MSP Expriy Background Service started");

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                _logger.LogInformation("checking membership at {Time}",
                DateTime.UtcNow);

                // Whatrerver the we have to write

                using var scope = _scopeFactory.CreateScope(); // it creates the scope


                //  var membershipRepository =
                //     scope.ServiceProvider.GetRequiredService<IMembershipRepository>();

                //       var expiringMemberships =
                //     await membershipRepository.GetExpiringMembershipsAsync();

                // _logger.LogInformation(
                //     "Found {Count} memberships expiring.",
                //     expiringMemberships.Count);

                // foreach (var membership in expiringMemberships)
                // {
                //     // Here we are shoing in log. 
                //     //But we will save in DB
                //     _logger.LogInformation(
                //         "Membership {MembershipId} of Member {MemberId} expires on {Date}.",
                //         membership.Id,
                //         membership.MemberId,
                //         membership.EndDate);
                // }


            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex, "An error occured MExpiry");
            }
            await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
        }


        _logger.LogInformation("MSP Expriy Background Service Ending");


    }


}


