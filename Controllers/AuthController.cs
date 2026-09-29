using InsurancePortalRegistration.DTOs;
using InsurancePortalRegistration.Services;
using Microsoft.AspNetCore.Mvc;

namespace InsurancePortalRegistration.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IRegistrationService registrationService;

    public AuthController(IRegistrationService registrationService)
    {
        this.registrationService = registrationService;
    }

    [HttpPost("validate-policyholder")]
    public async Task<IActionResult> ValidatePolicyholder(PolicyholderValidationRequest request)
    {
        var result = await registrationService.ValidatePolicyholderAsync(request);

        if (result == null)
        {
            return BadRequest(new
            {
                message = "Policyholder information could not be validated."
            });
        }

        return Ok(new
        {
            message = "Policyholder validated successfully."
        });
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(
        RegisterRequest request)
    {
        var result =
            await registrationService.RegisterAsync(request);

        if (result == null)
        {
            return BadRequest(new
            {
                message = "Policyholder information could not be validated."
            });
        }

        if (result == "POLICYHOLDER_ALREADY_REGISTERED")
        {
            return BadRequest(new
            {
                message = "User already exists. Please continue with login."
            });
        }

        if (result == "USERNAME_EXISTS")
        {
            return BadRequest(new
            {
                message = "Username already exists."
            });
        }

        return Ok(new
        {
            message = "Customer registered successfully."
        });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(
        LoginRequest request)
    {
        var account =
            await registrationService.LoginAsync(request);

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