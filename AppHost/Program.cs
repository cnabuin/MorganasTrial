IDistributedApplicationBuilder builder = DistributedApplication.CreateBuilder(args);

string umbracoName = "umbracocms";
string deliveryApiKey = "myApiKey";

IResourceBuilder<ProjectResource> cms = builder.AddProject<Projects.UmbracoCMS>(umbracoName)
    .WithEnvironment("UMBRACO_DELIVERY_API_KEY", deliveryApiKey)
    .WithHttpEndpoint()
    .WithHttpsEndpoint();

builder.AddProject<Projects.UmbracoBridge>("umbracobridge")
    .WithEnvironment("API_BASE_ADDRESS", $"https://{umbracoName}")
    .WithEnvironment("UMBRACO_DELIVERY_API_KEY", deliveryApiKey)
    .WithReference(cms)
    .WaitFor(cms);

builder.Build().Run();
