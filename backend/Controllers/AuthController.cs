using InsurancePortalRegistration.DTOs;
using InsurancePortalRegistration.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace InsurancePortalRegistration.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IRegistrationService registrationService;
    private readonly IAuthService authService;

    public AuthController(
    IRegistrationService registrationService,
    IAuthService authService)
{
    this.registrationService = registrationService;
    this.authService = authService;
}

    [HttpPost("validate-policyholder")]
public async Task<IActionResult> ValidatePolicyholder(
    PolicyholderValidationRequest request)
{
    var result =
        await registrationService.ValidatePolicyholderAsync(request);

    switch (result)
    {
        case PolicyholderValidationResult.Validated:
            return Ok(new
            {
                message = "Policyholder validated successfully."
            });

        case PolicyholderValidationResult.NotFound:
            return UnprocessableEntity(new
            {
                message = "Policyholder information could not be validated."
            });

        default:
            return StatusCode(StatusCodes.Status500InternalServerError);
    }
}

    [HttpPost("register")]
public async Task<IActionResult> Register(RegisterRequest request)
{
    var result =
        await registrationService.RegisterAsync(request);

    switch (result)
    {
        case RegistrationResult.PolicyholderNotFound:
            return UnprocessableEntity(new
            {
                message = "Policyholder information could not be validated."
            });

        case RegistrationResult.PolicyholderAlreadyRegistered:
            return Conflict(new
            {
                message = "User already exists. Please continue with login."
            });

        case RegistrationResult.UsernameExists:
            return Conflict(new
            {
                message = "Username already exists."
            });

        case RegistrationResult.Registered:
            return StatusCode(StatusCodes.Status201Created, new
            {
                message = "Customer registered successfully."
            });

        default:
            return StatusCode(StatusCodes.Status500InternalServerError);
    }
}

    [HttpPost("login")]
    [EnableRateLimiting("Login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var account =
            await authService.LoginAsync(request);

        if (account == null)
        {
            return Unauthorized(new
            {
                message = "Invalid username or password."
            });
        }

        return Ok(new
        {
            message = "Login successful.",
            username = account.Username,
            policyholderId = account.PolicyholderId
        });
    }
}