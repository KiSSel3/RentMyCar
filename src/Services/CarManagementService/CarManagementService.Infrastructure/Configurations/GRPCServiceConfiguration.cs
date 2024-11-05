using CarManagementService.Infrastructure.Services.Server;
using Microsoft.AspNetCore.Builder;

namespace CarManagementService.Infrastructure.Configurations;

public static class GRPCServiceConfiguration
{
    public static WebApplication UseGRPCConfiguration(this WebApplication app)
    {
        app.MapGrpcService<GRPCRentOfferService>();

        return app;
    }
}