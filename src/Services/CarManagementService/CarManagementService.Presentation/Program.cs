using CarManagementService.Application;
using CarManagementService.Infrastructure;
using CarManagementService.Presentation;

var builder = WebApplication.CreateBuilder(args);

//TODO: Temporary
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddApplicationServices();
builder.Services.AddPresentationServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();