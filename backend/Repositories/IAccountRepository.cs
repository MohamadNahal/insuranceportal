using InsurancePortalRegistration.Models;

namespace InsurancePortalRegistration.Repositories;

public interface IAccountRepository
{
    Task<bool> UsernameExistsAsync(string username);
    Task<bool> PolicyholderHasAccountAsync(Guid policyholderId);
    Task AddAccountAsync(Account account);
    Task<Account?> FindByUsernameAsync(string username);
}