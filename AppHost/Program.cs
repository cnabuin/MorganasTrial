IDistributedApplicationBuilder builder = DistributedApplication.CreateBuilder(args);


IResourceBuilder<ProjectResource> cms = builder.AddProject<Projects.UmbracoCMS>("umbracocms")
    .WithHttpEndpoint()
    .WithHttpsEndpoint();

IResourceBuilder<ProjectResource> bridge = builder.AddProject<Projects.UmbracoBridge>("umbracobridge")
    .WithReference(cms)
    .WaitFor(cms);

builder.Build().Run();
