using System.Text.Json;
using InsurancePortalRegistration.Models;

namespace InsurancePortalRegistration.Repositories;

public class PolicyholderRepository : IPolicyholderRepository
{
    private readonly string _filePath = "Data/policyholders.json";

    public async Task<Policyholder?> FindPolicyholderAsync(
        string ssn,
        string policyNumber,
        string firstName,
        string lastName,
        string dateOfBirth,
        string zipCode)
    {
        var json = await File.ReadAllTextAsync(_filePath);

        var policyData =
            JsonSerializer.Deserialize<PolicyData>(
                json,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

        if (policyData == null)
        {
            return null;
        }

        return policyData.Policies.FirstOrDefault(policy =>
            policy.SSN.Equals(
                ssn,
                StringComparison.OrdinalIgnoreCase) &&
            policy.PolicyNumber.Equals(
                policyNumber,
                StringComparison.OrdinalIgnoreCase) &&
            policy.FirstName.Equals(
                firstName,
                StringComparison.OrdinalIgnoreCase) &&
            policy.LastName.Equals(
                lastName,
                StringComparison.OrdinalIgnoreCase) &&
            policy.DateOfBirth.Equals(
                dateOfBirth,
                StringComparison.OrdinalIgnoreCase) &&
            policy.ZipCode.Equals(
                zipCode,
                StringComparison.OrdinalIgnoreCase));
    }

    public async Task<Policyholder?> FindPolicyholderByIdAsync(
        Guid id)
    {
        var json = await File.ReadAllTextAsync(_filePath);

        var policyData =
            JsonSerializer.Deserialize<PolicyData>(
                json,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

        if (policyData == null)
        {
            return null;
        }

        return policyData.Policies.FirstOrDefault(
            policy => policy.Id == id);
    }
}