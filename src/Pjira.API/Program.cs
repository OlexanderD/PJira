using Microsoft.Extensions.DependencyInjection.Extensions;
using Pjira.Api;
using Pjira.Application;
using Pjira.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddApiServices()
    .AddApplicationServices()
    .AddInfrustructureServices(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Your API V1");
        c.RoutePrefix = string.Empty;
    });
}

app.MapControllers();

app.Run();
