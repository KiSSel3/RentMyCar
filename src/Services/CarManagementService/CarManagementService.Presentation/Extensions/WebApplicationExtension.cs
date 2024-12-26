using CarManagementService.Infrastructure.Configurations;
using CarManagementService.Presentation.Hubs;
using CarManagementService.Presentation.Middlewares;
using Microsoft.AspNetCore.HttpOverrides;
using Serilog;

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
        
        app.UseForwardedHeaders(new ForwardedHeadersOptions {
            ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
        });
        
        app.UseHttpsRedirection();
        app.UseHsts();
        
        app.UseStaticFiles();
        
        app.UseSerilogRequestLogging();
        
        app.UseRouting();
        
        app.UseAuthentication();
        app.UseAuthorization();

        app.UseGRPCConfiguration();
        
        app.MapControllers();

        app.MapHub<ReviewHub>("/hubs/reviews");
        
        return app;
    }
}