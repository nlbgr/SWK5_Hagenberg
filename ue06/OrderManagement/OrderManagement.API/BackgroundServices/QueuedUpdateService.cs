using OrderManagement.Logic;

namespace OrderManagement.Api.BackgroundServices;

public class QueuedUpdateService : BackgroundService
{
    private readonly ILogger<QueuedUpdateService> logger;
    private readonly IServiceProvider serviceProvider;
    private readonly UpdateChannel updateChannel;

    public QueuedUpdateService(ILogger<QueuedUpdateService> logger, IServiceProvider serviceProvider, UpdateChannel updateChannel)
    {
        this.logger = logger;
        this.serviceProvider = serviceProvider;
        this.updateChannel = updateChannel;
    }

    protected async override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        // using var scope = serviceProvider.CreateScope();
        // var logic = scope.ServiceProvider.GetRequiredService<IOrderManagementLogic>();
        //
        // while (!stoppingToken.IsCancellationRequested)
        // {
        //
        //     logger.LogInformation($"ExecuteAsync executed");
        //     await Task.Delay(1000);
        // }
        
        await foreach (var customerId in updateChannel.ReadAllAsync(stoppingToken))
        {
            using var scope = serviceProvider.CreateScope();
            var logic = scope.ServiceProvider.GetRequiredService<IOrderManagementLogic>();

            await logic.UpdateTotalRevenueAsync(customerId);
            logger.LogInformation($"Updated total revenue for customer {customerId} at {DateTime.Now}");
        }
    }
}
