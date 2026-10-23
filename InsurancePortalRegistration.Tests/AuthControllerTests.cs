
using InsurancePortalRegistration.Controllers;
using InsurancePortalRegistration.DTOs;
using InsurancePortalRegistration.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace InsurancePortalRegistration.Tests;

public class AuthControllerTests
{
    private WebApplicationFactory<Program> factory = null!;
    private Mock<IRegistrationService> registrationService = null!;
    private Mock<IAuthService> authService = null!;
    private AuthController controller = null!;

    [SetUp]
    public void SetUp()
    {
        factory = new WebApplicationFactory<Program>();

        registrationService = new Mock<IRegistrationService>();
        authService = new Mock<IAuthService>();

        var problemDetailsFactory = factory.Services.GetRequiredService<ProblemDetailsFactory>();

        controller = new AuthController(
            registrationService.Object,
            authService.Object,
            problemDetailsFactory);

        controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext()
        };
    }

    [TearDown]
    public void TearDown()
    {
        factory.Dispose();
    }

    [Test]
    public async Task ValidatePolicyholder_WhenValidated_ReturnsOk()
    {
        registrationService
            .Setup(service => service.ValidatePolicyholderAsync(
                It.IsAny<PolicyholderValidationRequest>()))
            .ReturnsAsync(PolicyholderValidationResult.Validated);

        var result = await controller.ValidatePolicyholder(
            new PolicyholderValidationRequest());

        Assert.That(result, Is.InstanceOf<OkObjectResult>());
    }

    [Test]
    public async Task ValidatePolicyholder_WhenNotFound_Returns422()
    {
        registrationService
            .Setup(service => service.ValidatePolicyholderAsync(
                It.IsAny<PolicyholderValidationRequest>()))
            .ReturnsAsync(PolicyholderValidationResult.NotFound);

        var result = await controller.ValidatePolicyholder(
            new PolicyholderValidationRequest());

        Assert.That(result, Is.InstanceOf<ObjectResult>());
        Assert.That(
            ((ObjectResult)result).StatusCode,
            Is.EqualTo(StatusCodes.Status422UnprocessableEntity));
    }

    [Test]
    public async Task ValidatePolicyholder_WhenAlreadyRegistered_ReturnsConflict()
    {
        registrationService
            .Setup(service => service.ValidatePolicyholderAsync(
                It.IsAny<PolicyholderValidationRequest>()))
            .ReturnsAsync(PolicyholderValidationResult.AlreadyRegistered);

        var result = await controller.ValidatePolicyholder(
            new PolicyholderValidationRequest());

        Assert.That(result, Is.InstanceOf<ObjectResult>());
        Assert.That(
            ((ObjectResult)result).StatusCode,
            Is.EqualTo(StatusCodes.Status409Conflict));
    }

    [Test]
    public async Task Register_WhenRegistered_ReturnsCreated()
    {
        registrationService
            .Setup(service => service.RegisterAsync(
                It.IsAny<RegisterRequest>()))
            .ReturnsAsync(RegistrationResult.Registered);

        var result = await controller.Register(new RegisterRequest());

        Assert.That(result, Is.InstanceOf<ObjectResult>());
        Assert.That(
            ((ObjectResult)result).StatusCode,
            Is.EqualTo(StatusCodes.Status201Created));
    }

    [Test]
    public async Task Register_WhenPolicyholderNotFound_Returns422()
    {
        registrationService
            .Setup(service => service.RegisterAsync(
                It.IsAny<RegisterRequest>()))
            .ReturnsAsync(RegistrationResult.PolicyholderNotFound);

        var result = await controller.Register(new RegisterRequest());

        Assert.That(result, Is.InstanceOf<ObjectResult>());
        Assert.That(
            ((ObjectResult)result).StatusCode,
            Is.EqualTo(StatusCodes.Status422UnprocessableEntity));
    }

    [Test]
    public async Task Register_WhenPolicyholderAlreadyRegistered_ReturnsConflict()
    {
        registrationService
            .Setup(service => service.RegisterAsync(
                It.IsAny<RegisterRequest>()))
            .ReturnsAsync(RegistrationResult.PolicyholderAlreadyRegistered);

        var result = await controller.Register(new RegisterRequest());

        Assert.That(result, Is.InstanceOf<ObjectResult>());
        Assert.That(
            ((ObjectResult)result).StatusCode,
            Is.EqualTo(StatusCodes.Status409Conflict));
    }

    [Test]
    public async Task Register_WhenUsernameExists_ReturnsConflict()
    {
        registrationService
            .Setup(service => service.RegisterAsync(
                It.IsAny<RegisterRequest>()))
            .ReturnsAsync(RegistrationResult.UsernameExists);

        var result = await controller.Register(new RegisterRequest());

        Assert.That(result, Is.InstanceOf<ObjectResult>());
        Assert.That(
            ((ObjectResult)result).StatusCode,
            Is.EqualTo(StatusCodes.Status409Conflict));
    }

    [Test]
    public async Task Login_WhenCredentialsAreInvalid_ReturnsUnauthorized()
    {
        authService
            .Setup(service => service.LoginAsync(
                It.IsAny<LoginRequest>()))
            .ReturnsAsync((LoginResponse?)null);

        var result = await controller.Login(new LoginRequest
        {
            Username = "unknownuser",
            Password = "WrongPassword123!"
        });

        Assert.That(result, Is.InstanceOf<ObjectResult>());
        Assert.That(
            ((ObjectResult)result).StatusCode,
            Is.EqualTo(StatusCodes.Status401Unauthorized));
    }
}
