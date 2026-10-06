using InsurancePortalRegistration.DTOs;
using InsurancePortalRegistration.Entities;

namespace InsurancePortalRegistration.Services;

public interface IAuthService
{
    Task<Account?> LoginAsync(LoginRequest request);
}