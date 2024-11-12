using CarManagementService.Infrastructure.Configurations;
using CarManagementService.Presentation.Middlewares;

namespace CarManagementService.Presentation.Extensions;

public static class WebApplicationExtension
{
    public static WebApplication AddSwagger(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        return app;
    }
    
    public static WebApplication AddApplicationMiddleware(this WebApplication app)
    {
        app.UseMiddleware<GlobalExceptionHandlingMiddleware>();
        
        app.UseStaticFiles();
        app.UseRouting();
        
        app.UseAuthentication();
        app.UseAuthorization();

        app.UseGRPCConfiguration();
        
        app.MapControllers();

        return app;
    }
}