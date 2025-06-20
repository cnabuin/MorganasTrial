IDistributedApplicationBuilder builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.UmbracoCMS>("umbracocms");
builder.AddProject<Projects.UmbracoBridge>("umbracobridge");

builder.Build().Run();
