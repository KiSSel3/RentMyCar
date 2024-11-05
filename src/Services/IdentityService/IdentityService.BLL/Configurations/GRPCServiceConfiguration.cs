using IdentityService.BLL.External.Services;
using Microsoft.AspNetCore.Builder;

namespace IdentityService.BLL.Configurations;

public static class GRPCServiceConfiguration
{
    public static WebApplication UseGRPCConfiguration(this WebApplication app)
    {
        app.MapGrpcService<GRPCUserService>();

        return app;
    }
}