using InsurancePortalRegistration.Data;
using InsurancePortalRegistration.Exceptions;
using InsurancePortalRegistration.Models;
using Microsoft.EntityFrameworkCore;

namespace InsurancePortalRegistration.Repositories;

public class SqlAccountRepository : IAccountRepository
{
    private readonly InsurancePortalDbContext context;
    private readonly ILogger<SqlAccountRepository> logger;

    public SqlAccountRepository(InsurancePortalDbContext context,ILogger<SqlAccountRepository> logger)
    {
        this.context = context;
        this.logger = logger;
    }

    public async Task<bool> UsernameExistsAsync(string username)
    {
        try
        {
            return await context.Accounts
                .AnyAsync(account =>
                    account.Username == username);
        }
        catch (Exception ex)
        {
            logger.LogError(
                ex,
                "Could not check whether username exists.");

            throw new CouldNotFindAccountException();
        }
    }

    public async Task<bool> PolicyholderHasAccountAsync(Guid policyholderId)
    {
        try
        {
            return await context.Accounts
                .AnyAsync(account =>
                    account.PolicyholderId == policyholderId);
        }
        catch (Exception ex)
        {
            logger.LogError(
                ex,
                "Could not check whether policyholder has an account.");

            throw new CouldNotFindAccountException();
        }
    }

    public async Task AddAccountAsync(Account account)
    {
        try
        {
            await context.Accounts.AddAsync(account);
            await context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            logger.LogError(
                ex,
                "Could not add account.");

            throw new CouldNotAddAccountException();
        }
    }

    public async Task<Account?> FindByUsernameAsync(string username)
    {
        try
        {
            return await context.Accounts
                .FirstOrDefaultAsync(account =>
                    account.Username == username);
        }
        catch (Exception ex)
        {
            logger.LogError(
                ex,
                "Could not find account by username.");

            throw new CouldNotFindAccountException();
        }
    }
}