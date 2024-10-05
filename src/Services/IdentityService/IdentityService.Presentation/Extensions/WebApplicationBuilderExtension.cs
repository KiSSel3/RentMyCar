using IdentityService.BLL;
using IdentityService.DAL;

namespace IdentityService.Presentation.Extensions;

public static class WebApplicationBuilderExtension
{
    public static void AddServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        
        builder.Services.AddDataAccessLayerServices(builder.Configuration);
        builder.Services.AddBusinessLogicLayerServices(builder.Configuration);
    }
}