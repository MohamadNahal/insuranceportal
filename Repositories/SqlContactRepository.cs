using InsurancePortalRegistration.Data;
using InsurancePortalRegistration.Models;

namespace InsurancePortalRegistration.Repositories;

public class SqlContactRepository : IContactRepository
{
    private readonly InsurancePortalDbContext context;

    public SqlContactRepository(InsurancePortalDbContext context)
    {
        this.context = context;
    }

    public async Task AddContactAsync(ContactInformation contact)
    {
        await context.Contacts.AddAsync(contact);
        await context.SaveChangesAsync();
    }
}