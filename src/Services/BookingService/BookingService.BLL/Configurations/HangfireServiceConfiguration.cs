using BookingService.BLL.BackgroundJobs;
using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

namespace BookingService.BLL.Configurations;

public static class HangfireServiceConfiguration
{
    public static IApplicationBuilder UseHangfireConfiguration(this IApplicationBuilder app)
    {
        app.UseHangfireDashboard("/hangfire", new DashboardOptions
        {
            DashboardTitle = "Booking Service Jobs",
            DisplayStorageConnectionString = false,
            IsReadOnlyFunc = (context) => false
        });
        
        RecurringJob.AddOrUpdate<UnsentNotificationsJob>(
            "process-unsent-notifications",
            job => job.ProcessUnsentNotificationsAsync(CancellationToken.None),
            "*/30 * * * *"
        );
        
        return app;
    }
}