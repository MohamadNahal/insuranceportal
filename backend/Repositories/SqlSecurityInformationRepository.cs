using InsurancePortalRegistration.Data;
using InsurancePortalRegistration.Exceptions;
using InsurancePortalRegistration.Models;

namespace InsurancePortalRegistration.Repositories;

public class SqlSecurityInformationRepository
    : ISecurityInformationRepository
{
    private readonly InsurancePortalDbContext context;
    private readonly ILogger<SqlSecurityInformationRepository> logger;

    public SqlSecurityInformationRepository(InsurancePortalDbContext context,ILogger<SqlSecurityInformationRepository> logger)
    {
        this.context = context;
        this.logger = logger;
    }

    public async Task AddSecurityInformationAsync(SecurityInformation securityInformation)
    {
        try
        {
            await context.SecurityInformation.AddAsync(
                securityInformation);

            await context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            logger.LogError(
                ex,
                "Could not add security information.");

            throw new CouldNotAddSecurityInformationException();
        }
    }
}