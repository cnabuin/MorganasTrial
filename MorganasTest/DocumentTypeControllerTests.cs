using Microsoft.AspNetCore.Mvc;
using Moq;
using UmbracoBridge.Services;

namespace UmbracoBridge.Controllers;

public class UmbracoManagementControllerTests
{
    private readonly Mock<IUmbracoManagementService> _umbracoManagementServiceMock;
    private readonly Mock<IDocumentTypeService> _documentTypeServiceMock;
    private readonly UmbracoManagementController _target;

    public UmbracoManagementControllerTests()
    {
        _umbracoManagementServiceMock = new Mock<IUmbracoManagementService>();
        _documentTypeServiceMock = new Mock<IDocumentTypeService>();
        _target = new UmbracoManagementController(_umbracoManagementServiceMock.Object, _documentTypeServiceMock.Object);
    }

    [Fact]
    public async Task Post_ShouldReturnOk_WhenServiceCompletesSuccessfully()
    {
        // Arrange
        CreateDocumentTypeRequestModel requestModel = new();
        string expectedResponse = Guid.NewGuid().ToString();

        _documentTypeServiceMock.Setup(service => service.Create(requestModel))
            .ReturnsAsync(expectedResponse);

        // Act
        ObjectResult result = await _target.Post(requestModel);

        // Assert
        OkObjectResult okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(200, okResult.StatusCode);
        Assert.Equal(expectedResponse, okResult.Value);
    }

    [Fact]
    public async Task Post_ShouldReturnApiExceptionStatusCodeAndDetails_WhenServiceThrowsApiException()
    {
        // Arrange
        CreateDocumentTypeRequestModel requestModel = new();
        ApiException apiException = new(400, new ProblemDetails());
        _documentTypeServiceMock.Setup(service => service.Create(requestModel))
            .ThrowsAsync(apiException);

        // Act
        ObjectResult result = await _target.Post(requestModel);

        // Assert
        ObjectResult statusCodeResult = Assert.IsType<ObjectResult>(result);
        Assert.Equal(apiException.StatusCode, statusCodeResult.StatusCode);
        Assert.Equal(apiException.ProblemDetails, statusCodeResult.Value);
    }

    [Fact]
    public async Task Post_ShouldReturnApiExceptionStatusCode_WhenServiceThrowsApiExceptionWithoutProblemDetails()
    {
        // Arrange
        CreateDocumentTypeRequestModel requestModel = new();
        ApiException apiException = new ApiException(400, null);
        _documentTypeServiceMock.Setup(service => service.Create(requestModel))
            .ThrowsAsync(apiException);

        // Act
        ObjectResult result = await _target.Post(requestModel);

        // Assert
        ObjectResult statusCodeResult = Assert.IsType<ObjectResult>(result);
        Assert.Equal(apiException.StatusCode, statusCodeResult.StatusCode);
        Assert.Null(statusCodeResult.Value);
    }

    [Fact]
    public async Task Delete_ShouldReturnOk_WhenServiceFinishesSuccessfully()
    {
        // Arrange
        string documentTypeId = "123";
        _documentTypeServiceMock.Setup(service => service.Delete(documentTypeId))
            .Returns(Task.CompletedTask);

        // Act
        ActionResult result = await _target.Delete(documentTypeId);

        // Assert
        OkResult okResult = Assert.IsType<OkResult>(result);
        Assert.Equal(200, okResult.StatusCode);
    }

    [Fact]
    public async Task Delete_ShouldReturnApiExceptionStatusCodeAndDetails_WhenServiceThrowsApiException()
    {
        // Arrange
        string documentTypeId = "123";
        ApiException apiException = new ApiException(404, new ProblemDetails());
        _documentTypeServiceMock.Setup(service => service.Delete(documentTypeId))
            .ThrowsAsync(apiException);

        // Act
        ActionResult result = await _target.Delete(documentTypeId);

        // Assert
        ObjectResult statusCodeResult = Assert.IsType<ObjectResult>(result);
        Assert.Equal(apiException.StatusCode, statusCodeResult.StatusCode);
        Assert.Equal(apiException.ProblemDetails, statusCodeResult.Value);
    }

    [Fact]
    public async Task Delete_ShouldReturnApiExceptionStatusCode_WhenServiceThrowsApiExceptionWithoutProblemDetails()
    {
        // Arrange
        string documentTypeId = "123";
        ApiException apiException = new ApiException(404, null);
        _documentTypeServiceMock.Setup(service => service.Delete(documentTypeId))
            .ThrowsAsync(apiException);

        // Act
        ActionResult result = await _target.Delete(documentTypeId);

        // Assert
        ObjectResult statusCodeResult = Assert.IsType<ObjectResult>(result);
        Assert.Equal(apiException.StatusCode, statusCodeResult.StatusCode);
        Assert.Equal(apiException.ProblemDetails, statusCodeResult.Value);
    }
}