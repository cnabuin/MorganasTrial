using Umbraco.Cms.Core.Configuration.Models;

public static class UmbracoConfigurationExtensions
{
    public static IUmbracoBuilder ConfigureDeliveryApi(
        this IUmbracoBuilder builder,
        IConfiguration configuration)
    {
        builder.Services.Configure<DeliveryApiSettings>(options =>
        {
            options.Enabled = true;
            options.ApiKey = configuration["UMBRACO_DELIVERY_API_KEY"]
                ?? throw new InvalidOperationException("Delivery API key not configured");
        });

        return builder;
    }
}