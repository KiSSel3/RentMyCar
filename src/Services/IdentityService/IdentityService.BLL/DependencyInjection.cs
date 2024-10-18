using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;
using IdentityService.BLL.Models.Options;
using IdentityService.BLL.Providers.Implementation;
using IdentityService.BLL.Providers.Interfaces;
using IdentityService.BLL.Services.Implementation;
using IdentityService.BLL.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityService.BLL;

public static class DependencyInjection
{

    public static IServiceCollection AddBusinessLogicLayerServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services.Configure<TokenOptions>(configuration.GetSection(TokenOptions.DefaultSection));

        services.AddScoped<ITokenProvider, TokenProvider>();
        
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IRoleService, RoleService>();
            
        return services;
    }
}