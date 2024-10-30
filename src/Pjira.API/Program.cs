using Pjira.Api;
using Pjira.Application;
using Pjira.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddApiServices(builder.Configuration)
    .AddApplicationServices()
    .AddInfrustructureServices(builder.Configuration);

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Your API V1");
    c.RoutePrefix = string.Empty;
});

app.MapControllers();

app.Run();
