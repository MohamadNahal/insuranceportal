using InsurancePortalRegistration.DTOs;

namespace InsurancePortalRegistration.Services;

public interface IRegistrationService
{
    Task<PolicyholderValidationResult> ValidatePolicyholderAsync(
        PolicyholderValidationRequest request);

    Task<RegistrationResult> RegisterAsync(RegisterRequest request);
}