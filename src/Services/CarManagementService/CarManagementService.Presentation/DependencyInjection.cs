using System.Reflection;
using System.Text.Json.Serialization;
using CarManagementService.Presentation.Infrastructure.Filters;
using Microsoft.OpenApi.Models;

namespace CarManagementService.Presentation;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentationServices(this IServiceCollection services)
    {
        services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });
        
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services.AddEndpointsApiExplorer();
        
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

            //c.OperationFilter<AddValidationErrorsToSwagger>();
        });

        return services;
    }
}