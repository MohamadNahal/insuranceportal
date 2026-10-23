
using System.Text.Json;
using InsurancePortalRegistration.ExceptionHandling;
using InsurancePortalRegistration.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace InsurancePortalRegistration.Tests;

public class GlobalExceptionHandlerTests
{
    private GlobalExceptionHandler handler = null!;
    private DefaultHttpContext httpContext = null!;

    [SetUp]
    public void SetUp()
    {
        var logger = new Mock<ILogger<GlobalExceptionHandler>>();

        handler = new GlobalExceptionHandler(logger.Object);

        httpContext = new DefaultHttpContext();
        httpContext.Response.Body = new MemoryStream();
    }

    [Test]
    public async Task TryHandleAsync_WhenDuplicateRegistration_Returns409()
    {
        var exception = new DuplicateRegistrationException(
            "This registration already exists.");
        var handled = await handler.TryHandleAsync(
            httpContext,
            exception,
            CancellationToken.None);
        Assert.That(handled, Is.True);
        Assert.That(
            httpContext.Response.StatusCode,
            Is.EqualTo(StatusCodes.Status409Conflict));

        Assert.That(
            httpContext.Response.ContentType,
            Does.StartWith("application/problem+json"));

        var problem = await ReadResponseAsync();

        Assert.That(
            problem.RootElement.GetProperty("status").GetInt32(),
            Is.EqualTo(409));

        Assert.That(
            problem.RootElement.GetProperty("title").GetString(),
            Is.EqualTo("Registration conflict."));

        Assert.That(
            problem.RootElement.GetProperty("detail").GetString(),
            Is.EqualTo("This registration already exists."));
    }

    [Test]
    public async Task TryHandleAsync_WhenUnexpectedException_Returns500()
    {
        var exception = new InvalidOperationException(
            "Sensitive internal details.");
        var handled = await handler.TryHandleAsync(
            httpContext,
            exception,
            CancellationToken.None);
        Assert.That(handled, Is.True);
        Assert.That(
            httpContext.Response.StatusCode,
            Is.EqualTo(StatusCodes.Status500InternalServerError));

        Assert.That(
            httpContext.Response.ContentType,
            Does.StartWith("application/problem+json"));

        var problem = await ReadResponseAsync();

        Assert.That(
            problem.RootElement.GetProperty("status").GetInt32(),
            Is.EqualTo(500));

        Assert.That(
            problem.RootElement.GetProperty("title").GetString(),
            Is.EqualTo("An unexpected error occurred."));

        Assert.That(
            problem.RootElement.GetProperty("detail").GetString(),
            Is.EqualTo("An unexpected error occurred."));

        Assert.That(
            problem.RootElement.GetRawText(),
            Does.Not.Contain("Sensitive internal details."));
    }

    [Test]
    public async Task TryHandleAsync_LogsException()
    {
        var logger = new Mock<ILogger<GlobalExceptionHandler>>();
        handler = new GlobalExceptionHandler(logger.Object);

        var exception = new InvalidOperationException("Test exception");
        await handler.TryHandleAsync(
            httpContext,
            exception,
            CancellationToken.None);
        logger.Verify(
            x => x.Log(
                LogLevel.Error,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((state, type) =>
                    state.ToString()!.Contains(
                        "An unhandled exception occurred.")),
                exception,
                It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
            Times.Once);
    }

    private async Task<JsonDocument> ReadResponseAsync()
    {
        httpContext.Response.Body.Position = 0;

        return await JsonDocument.ParseAsync(
            httpContext.Response.Body);
    }
}
