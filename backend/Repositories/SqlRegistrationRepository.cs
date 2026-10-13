
using InsurancePortalRegistration.Data;
using InsurancePortalRegistration.Exceptions;
using InsurancePortalRegistration.Entities;
using Microsoft.EntityFrameworkCore;

namespace InsurancePortalRegistration.Repositories;

public class SqlRegistrationRepository : IRegistrationRepository
{
    private readonly InsurancePortalDbContext context;
    private readonly ILogger<SqlRegistrationRepository> logger;

    public SqlRegistrationRepository(
        InsurancePortalDbContext context,
        ILogger<SqlRegistrationRepository> logger)
    {
        this.context = context;
        this.logger = logger;
    }

    public async Task AddRegistrationAsync(
        Account account,
        ContactInformation contact,
        SecurityInformation securityInformation)
    {
        try
        {
            context.Accounts.Add(account);
            context.Contacts.Add(contact);
            context.SecurityInformation.Add(securityInformation);

            await context.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            if (ex.InnerException is Microsoft.Data.SqlClient.SqlException sqlException &&
                (sqlException.Number == 2601 ||
                 sqlException.Number == 2627))
            {
                var message = sqlException.Message.Contains(
                    "Username",
                    StringComparison.OrdinalIgnoreCase)
                    ? "Username already exists."
                    : "A registration record conflicts with existing data.";

                logger.LogWarning(
                    ex,
                    "A duplicate-key conflict occurred during registration.");

                throw new DuplicateRegistrationException(message, ex);
            }

            logger.LogError(
                ex,
                "Could not complete registration.");

            throw;
        }
    }
}
