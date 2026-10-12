using InsurancePortalRegistration.DTOs;

namespace InsurancePortalRegistration.Services;

public interface IAuthService
{
    Task<LoginResponse?> LoginAsync(LoginRequest request);
}
