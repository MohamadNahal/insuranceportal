using InsurancePortalRegistration.DTOs;
using InsurancePortalRegistration.Models;

namespace InsurancePortalRegistration.Services;

public interface IRegistrationService
{
    Task<string?> ValidatePolicyholderAsync(PolicyholderValidationRequest request);

    Task<string?> RegisterAsync(RegisterRequest request);

    Task<Account?> LoginAsync(LoginRequest request);
}