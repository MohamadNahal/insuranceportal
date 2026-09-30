using InsurancePortalRegistration.Models;

namespace InsurancePortalRegistration.Repositories;

public interface IPolicyholderRepository
{
    Task<Policyholder?> FindPolicyholderAsync(
        string ssn,
        string policyNumber,
        string firstName,
        string lastName,
        string dateOfBirth,
        string zipCode);
    Task<Policyholder?> FindPolicyholderByIdAsync(Guid id);
}