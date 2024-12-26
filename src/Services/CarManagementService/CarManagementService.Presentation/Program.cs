using CarManagementService.Presentation.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.AddLogging()
    .AddServices()
    .ConfigureJWT()
    .AddAuthentication()
    .AddSwaggerDocumentation();

var app = builder.Build();

app.AddSwagger()
    .AddApplicationMiddleware()
    .Run();