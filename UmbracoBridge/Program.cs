using Scalar.AspNetCore;
using UmbracoBridge.Services;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddOpenApi();

// Turn on service discovery to resolve umbracocsm address via Aspire.Net orchestration.
builder.Services.AddServiceDiscovery();
builder.Services.ConfigureHttpClientDefaults(http =>
{
    http.AddServiceDiscovery();
});

builder.Services.AddHttpClient<UmbracoManagementService>(client =>
{
    client.BaseAddress = new Uri("https://umbracocms");
});

builder.Services.AddScoped<IHealthCheckService, HealthcheckService>();
builder.Services.AddScoped<IDocumentTypeService, DocumentTypeService>();
builder.Services.AddScoped<IAuthService, AuthService>();

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
