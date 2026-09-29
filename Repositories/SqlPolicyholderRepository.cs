using InsurancePortalRegistration.Data;
using InsurancePortalRegistration.Models;
using Microsoft.EntityFrameworkCore;

namespace InsurancePortalRegistration.Repositories;

public class SqlPolicyholderRepository : IPolicyholderRepository
{
    private readonly InsurancePortalDbContext context;

    public SqlPolicyholderRepository(
        InsurancePortalDbContext context)
    {
        this.context = context;
    }

    public async Task<Policyholder?> FindPolicyholderAsync(
        string ssn,
        string policyNumber,
        string firstName,
        string lastName,
        string dateOfBirth,
        string zipCode)
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

    public async Task<Policyholder?> FindPolicyholderByIdAsync(Guid id)
    {
        return await context.Policyholders
            .FirstOrDefaultAsync(
                policyholder => policyholder.Id == id);
    }
}