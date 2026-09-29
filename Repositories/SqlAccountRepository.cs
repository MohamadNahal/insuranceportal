using InsurancePortalRegistration.Data;
using InsurancePortalRegistration.Models;
using Microsoft.EntityFrameworkCore;

namespace InsurancePortalRegistration.Repositories;

public class SqlAccountRepository : IAccountRepository
{
    private readonly InsurancePortalDbContext context;

    public SqlAccountRepository(InsurancePortalDbContext context)
    {
        this.context = context;
    }

    public async Task<bool> UsernameExistsAsync(string username)
    {
        return await context.Accounts
            .AnyAsync(account =>
                account.Username == username);
    }

    public async Task<bool> PolicyholderHasAccountAsync(Guid policyholderId)
    {
        return await context.Accounts
            .AnyAsync(account =>
                account.PolicyholderId == policyholderId);
    }

    public async Task AddAccountAsync(Account account)
    {
        await context.Accounts.AddAsync(account);
        await context.SaveChangesAsync();
    }

    public async Task<Account?> FindByUsernameAsync(string username)
    {
        return await context.Accounts
            .FirstOrDefaultAsync(account =>
                account.Username == username);
    }
}