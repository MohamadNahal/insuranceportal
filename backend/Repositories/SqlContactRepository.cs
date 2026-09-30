using InsurancePortalRegistration.Data;
using InsurancePortalRegistration.Exceptions;
using InsurancePortalRegistration.Models;

namespace InsurancePortalRegistration.Repositories;

public class SqlContactRepository : IContactRepository
{
    private readonly InsurancePortalDbContext context;
    private readonly ILogger<SqlContactRepository> logger;

    public SqlContactRepository(InsurancePortalDbContext context,ILogger<SqlContactRepository> logger)
    {
        this.context = context;
        this.logger = logger;
    }

    public async Task AddContactAsync(ContactInformation contact)
    {
        try
        {
            await context.Contacts.AddAsync(contact);
            await context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            logger.LogError(
                ex,
                "Could not add contact information.");

            throw new CouldNotAddContactException();
        }
    }
}