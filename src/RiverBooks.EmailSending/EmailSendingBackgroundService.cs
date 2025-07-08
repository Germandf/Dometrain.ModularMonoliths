using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace RiverBooks.EmailSending;

internal class EmailSendingBackgroundService(
    ILogger<EmailSendingBackgroundService> logger,
    IOutboxEmailService outboxEmailService)
    : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var delay = TimeSpan.FromSeconds(10);

        logger.LogInformation("{ServiceName} started, waiting {Delay} before next execution",
            nameof(EmailSendingBackgroundService), delay);

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                await outboxEmailService.CheckAndSendEmails(stoppingToken);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "{ServiceName} encountered an error", nameof(EmailSendingBackgroundService));
            }
            finally
            {
                await Task.Delay(delay, stoppingToken);
            }
        }

        logger.LogInformation("{ServiceName} stopped",
            nameof(EmailSendingBackgroundService));
    }
}
