
using InsurancePortalRegistration.DTOs;
using InsurancePortalRegistration.Repositories;

namespace InsurancePortalRegistration.Services;

public class AuthService : IAuthService
{
    private readonly IAccountRepository accountRepository;

    public AuthService(IAccountRepository accountRepository)
    {
        this.accountRepository = accountRepository;
    }

    public async Task<LoginResponse?> LoginAsync(LoginRequest request)
    {
        var account =
            await accountRepository.FindByUsernameAsync(request.Username);

        if (account == null)
        {
            return null;
        }

        var passwordValid = BCrypt.Net.BCrypt.Verify(
            request.Password,
            account.PasswordHash);

        if (!passwordValid)
        {
            return null;
        }

        return new LoginResponse
        {
            Username = account.Username,
            PolicyholderId = account.PolicyholderId
        };
    }
}
