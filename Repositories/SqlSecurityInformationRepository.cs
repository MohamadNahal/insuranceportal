using InsurancePortalRegistration.Data;
using InsurancePortalRegistration.Models;

namespace InsurancePortalRegistration.Repositories;

public class SqlSecurityInformationRepository
    : ISecurityInformationRepository
{
    private readonly InsurancePortalDbContext context;

    public SqlSecurityInformationRepository(InsurancePortalDbContext context)
    {
        this.context = context;
    }

    public async Task AddSecurityInformationAsync(SecurityInformation securityInformation)
    {
        await context.SecurityInformation.AddAsync(
            securityInformation);

        await context.SaveChangesAsync();
    }
}