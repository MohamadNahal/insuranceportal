
using InsurancePortalRegistration.DTOs;
using InsurancePortalRegistration.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Security.Claims;

namespace InsurancePortalRegistration.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IRegistrationService registrationService;
    private readonly IAuthService authService;
    private readonly ProblemDetailsFactory problemDetailsFactory;

    public AuthController(
        IRegistrationService registrationService,
        IAuthService authService,
        ProblemDetailsFactory problemDetailsFactory)
    {
        this.registrationService = registrationService;
        this.authService = authService;
        this.problemDetailsFactory = problemDetailsFactory;
    }

    [HttpPost("validate-policyholder")]
    public async Task<IActionResult> ValidatePolicyholder(
        PolicyholderValidationRequest request)
    {
        var result =await registrationService.ValidatePolicyholderAsync(request);
        switch (result)
        {
            case PolicyholderValidationResult.Validated:
                return Ok(new
                {
                    message = "Policyholder validated successfully."
                });

            case PolicyholderValidationResult.NotFound:
                return CreateProblem(
                    StatusCodes.Status422UnprocessableEntity,
                    "Policyholder validation failed.",
                    "Policyholder information could not be validated.");

            case PolicyholderValidationResult.AlreadyRegistered:
                return CreateProblem(
                    StatusCodes.Status409Conflict,
                    "Registration conflict.",
                    "User already exists. Please continue with login.");
            default:
                return CreateProblem(
                    StatusCodes.Status500InternalServerError,
                    "An unexpected error occurred.",
                    "An unexpected error occurred.");
        }
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var result = await registrationService.RegisterAsync(request);

        switch (result)
        {
            case RegistrationResult.PolicyholderNotFound:
                return CreateProblem(
                    StatusCodes.Status422UnprocessableEntity,
                    "Policyholder validation failed.",
                    "Policyholder information could not be validated.");

            case RegistrationResult.PolicyholderAlreadyRegistered:
                return CreateProblem(
                    StatusCodes.Status409Conflict,
                    "Registration conflict.",
                    "User already exists. Please continue with login.");

            case RegistrationResult.UsernameExists:
                return CreateProblem(
                    StatusCodes.Status409Conflict,
                    "Registration conflict.",
                    "Username already exists.");

            case RegistrationResult.Registered:
                return StatusCode(
                    StatusCodes.Status201Created,
                    new
                    {
                        message = "Customer registered successfully."
                    });

            default:
                return CreateProblem(
                    StatusCodes.Status500InternalServerError,
                    "An unexpected error occurred.",
                    "An unexpected error occurred.");
        }
    }

    [HttpPost("login")]
    [EnableRateLimiting("Login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var response = await authService.LoginAsync(request);

        if (response == null)
        {
            return CreateProblem(
                StatusCodes.Status401Unauthorized,
                "Authentication failed.",
                "Invalid username or password.");
        }

        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, response.Username),
            new("PolicyholderId", response.PolicyholderId.ToString())
        };

        var identity = new ClaimsIdentity(
            claims,
            CookieAuthenticationDefaults.AuthenticationScheme);

        var principal = new ClaimsPrincipal(identity);

        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            principal);

        return Ok(response);
    }

    [HttpGet("me")]
    [Authorize]
    public IActionResult GetCurrentUser()
    {
        return Ok(new
        {
            username = User.Identity?.Name,
            policyholderId = User.FindFirst("PolicyholderId")?.Value
        });
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(
            CookieAuthenticationDefaults.AuthenticationScheme);

        return Ok(new
        {
            message = "Logged out successfully."
        });
    }

    private ObjectResult CreateProblem(
        int statusCode,
        string title,
        string detail)
    {
        var problem = problemDetailsFactory.CreateProblemDetails(
            HttpContext,
            statusCode: statusCode,
            title: title,
            detail: detail);

        return new ObjectResult(problem)
        {
            StatusCode = statusCode
        };
    }
}
