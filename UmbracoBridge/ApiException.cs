using Microsoft.AspNetCore.Mvc;

namespace UmbracoBridge;

public class ApiException : Exception
{
    public ApiException(int statusCode, ProblemDetails? problemDetails)
    {
        StatusCode = statusCode;
        ProblemDetails = problemDetails;
    }

    public int StatusCode { get; set; }
    public ProblemDetails? ProblemDetails { get; set; }
}