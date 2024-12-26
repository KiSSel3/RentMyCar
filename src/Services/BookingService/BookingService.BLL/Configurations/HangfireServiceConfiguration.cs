using BookingService.BLL.Features.Notifications.BackgroundJobs.Interfaces;
using Hangfire;
using Microsoft.AspNetCore.Builder;

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
        
        RecurringJob.AddOrUpdate<IUnsentNotificationsJob>(
            "process-unsent-notifications",
            job => job.ProcessUnsentNotificationsAsync(CancellationToken.None),
            "*/30 * * * *"
        );
        
        return app;
    }
}