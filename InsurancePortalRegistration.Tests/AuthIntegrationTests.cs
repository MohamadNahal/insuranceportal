
using InsurancePortalRegistration.DTOs;
using InsurancePortalRegistration.Services;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Net;
using System.Net.Http.Json;

namespace InsurancePortalRegistration.Tests;

public class AuthIntegrationTests
{
    [Test]
    public async Task GetCurrentUser_WithoutLogin_ReturnsUnauthorized()
    {
        await using var factory = new WebApplicationFactory<Program>();

        using var client = factory.CreateClient(
            new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });

        var response = await client.GetAsync("/api/Auth/me");

        Assert.That(
            response.StatusCode,
            Is.EqualTo(HttpStatusCode.Unauthorized));
    }

    [Test]
    public async Task Login_WithValidCredentials_SetsAuthenticationCookie()
    {
        await using var factory = CreateFactory();

        using var client = factory.CreateClient(
            new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });

        var response = await client.PostAsJsonAsync(
            "/api/Auth/login",
            new LoginRequest
            {
                Username = "testuser",
                Password = "Password123!"
            });

        Assert.That(
            response.StatusCode,
            Is.EqualTo(HttpStatusCode.OK));

        Assert.That(
            response.Headers.Contains("Set-Cookie"),
            Is.True);

        var cookies = response.Headers.GetValues("Set-Cookie");

        Assert.That(
            cookies.Any(value =>
                value.StartsWith("InsurancePortal.Auth=")),
            Is.True);
    }

    [Test]
    public async Task Login_ThenGetCurrentUser_ReturnsAuthenticatedUser()
    {
        await using var factory = CreateFactory();

        using var client = factory.CreateClient(
            new WebApplicationFactoryClientOptions
            {
                HandleCookies = true,
                AllowAutoRedirect = false
            });

        var loginResponse = await client.PostAsJsonAsync(
            "/api/Auth/login",
            new LoginRequest
            {
                Username = "testuser",
                Password = "Password123!"
            });

        Assert.That(
            loginResponse.StatusCode,
            Is.EqualTo(HttpStatusCode.OK));

        var currentUserResponse =
            await client.GetAsync("/api/Auth/me");

        Assert.That(
            currentUserResponse.StatusCode,
            Is.EqualTo(HttpStatusCode.OK));
    }

    [Test]
    public async Task Logout_AfterLogin_RejectsSubsequentCurrentUserRequest()
    {
        await using var factory = CreateFactory();

        using var client = factory.CreateClient(
            new WebApplicationFactoryClientOptions
            {
                HandleCookies = true,
                AllowAutoRedirect = false
            });

        var loginResponse = await client.PostAsJsonAsync(
            "/api/Auth/login",
            new LoginRequest
            {
                Username = "testuser",
                Password = "Password123!"
            });

        Assert.That(
            loginResponse.StatusCode,
            Is.EqualTo(HttpStatusCode.OK));

        var logoutResponse =
            await client.PostAsync("/api/Auth/logout", null);

        Assert.That(
            logoutResponse.StatusCode,
            Is.EqualTo(HttpStatusCode.OK));

        var currentUserResponse =
            await client.GetAsync("/api/Auth/me");

        Assert.That(
            currentUserResponse.StatusCode,
            Is.EqualTo(HttpStatusCode.Unauthorized));
    }
    
    [Test]
    public async Task Login_WhenRateLimitExceeded_ReturnsProblemDetails()
    {
        await using var factory = CreateFactory();

        using var client = factory.CreateClient(
            new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });

        HttpResponseMessage? response = null;

        for (var attempt = 1; attempt <= 6; attempt++)
        {
            response = await client.PostAsJsonAsync(
                "/api/Auth/login",
                new LoginRequest
                {
                    Username = "testuser",
                    Password = "Password123!"
                });

            if (attempt < 6)
            {
                Assert.That(
                    response.StatusCode,
                    Is.EqualTo(HttpStatusCode.OK));
                response.Dispose();
            }
        }

        Assert.That(
            response!.StatusCode,
            Is.EqualTo(HttpStatusCode.TooManyRequests));

        Assert.That(
            response.Content.Headers.ContentType?.MediaType,
            Is.EqualTo("application/problem+json"));

        var body = await response.Content.ReadFromJsonAsync<
            Microsoft.AspNetCore.Mvc.ProblemDetails>();

        Assert.That(body, Is.Not.Null);
        Assert.That(body!.Status, Is.EqualTo(429));
        Assert.That(body.Title, Is.EqualTo("Too many requests."));

        response.Dispose();
    }


    private static WebApplicationFactory<Program> CreateFactory()
    {
        return new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    services.RemoveAll<IAuthService>();

                    var authService =
                        new Moq.Mock<IAuthService>();

                    var loginResponse = new LoginResponse
                    {
                        Username = "testuser",
                        PolicyholderId = Guid.NewGuid()
                    };

                    authService
                        .Setup(service => service.LoginAsync(
                            Moq.It.Is<LoginRequest>(request =>
                                request.Username == "testuser" &&
                                request.Password == "Password123!")))
                        .Returns(
                            Task.FromResult<LoginResponse?>(
                                loginResponse));

                    services.AddScoped<IAuthService>(
                        _ => authService.Object);
                });
            });
    }
}
