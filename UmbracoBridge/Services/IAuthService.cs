namespace UmbracoBridge.Services;

public interface IAuthService
{
    Task<string> GetAuthToken();
}