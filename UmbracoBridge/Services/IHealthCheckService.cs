namespace UmbracoBridge.Services;

public interface IHealthCheckService
{
    Task<object?> GetHealthChecks();
}
