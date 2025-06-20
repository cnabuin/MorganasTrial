IDistributedApplicationBuilder builder = DistributedApplication.CreateBuilder(args);

string umbracoName = "umbracocms";

IResourceBuilder<ProjectResource> cms = builder.AddProject<Projects.UmbracoCMS>(umbracoName)
    .WithHttpEndpoint()
    .WithHttpsEndpoint();

builder.AddProject<Projects.UmbracoBridge>("umbracobridge")
    .WithEnvironment("API_BASE_ADDRESS", $"https://{umbracoName}")
    .WithReference(cms)
    .WaitFor(cms);

builder.Build().Run();
