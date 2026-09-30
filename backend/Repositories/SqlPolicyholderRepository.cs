using InsurancePortalRegistration.Data;
using InsurancePortalRegistration.Exceptions;
using InsurancePortalRegistration.Models;
using Microsoft.EntityFrameworkCore;

namespace InsurancePortalRegistration.Repositories;

public class SqlPolicyholderRepository : IPolicyholderRepository
{
    private readonly InsurancePortalDbContext context;
    private readonly ILogger<SqlPolicyholderRepository> logger;

    public SqlPolicyholderRepository(InsurancePortalDbContext context,ILogger<SqlPolicyholderRepository> logger)
    {
        this.context = context;
        this.logger = logger;
    }

    public async Task<Policyholder?> FindPolicyholderAsync(
        string ssn,
        string policyNumber,
        string firstName,
        string lastName,
        string dateOfBirth,
        string zipCode)
    {
        try
        {
            return await context.Policyholders
                .FirstOrDefaultAsync(policyholder =>
                    policyholder.SSN == ssn &&
                    policyholder.PolicyNumber == policyNumber &&
                    policyholder.FirstName == firstName &&
                    policyholder.LastName == lastName &&
                    policyholder.DateOfBirth == dateOfBirth &&
                    policyholder.ZipCode == zipCode);
        }
        catch (Exception ex)
        {
            logger.LogError(
                ex,
                "Could not find policyholder.");

            throw new CouldNotFindPolicyholderException();
        }
    }

    public async Task<Policyholder?> FindPolicyholderByIdAsync(Guid id)
    {
        try
        {
            return await context.Policyholders
                .FirstOrDefaultAsync(
                    policyholder => policyholder.Id == id);
        }
        catch (Exception ex)
        {
            logger.LogError(
                ex,
                "Could not find policyholder by ID.");

            throw new CouldNotFindPolicyholderException();
        }
    }
}