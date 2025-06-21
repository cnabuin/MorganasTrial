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

var baseAddress = Environment.GetEnvironmentVariable("UMBRACO_BASE_ADDRESS");

builder.Services.AddHttpClient<IAuthService, AuthService>(client =>
{
    client.BaseAddress = new Uri(baseAddress);
});

builder.Services.AddHttpClient<IUmbracoManagementService, UmbracoManagementService>(client =>
{
    client.BaseAddress = new Uri(baseAddress);
});

builder.Services.AddHttpClient<IDocumentTypeService, DocumentTypeService>(client =>
{
    client.BaseAddress = new Uri(baseAddress);
});

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(string.Empty);
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
