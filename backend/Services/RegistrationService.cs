using InsurancePortalRegistration.DTOs;
using InsurancePortalRegistration.Entities;
using InsurancePortalRegistration.Repositories;

namespace InsurancePortalRegistration.Services;

public class RegistrationService : IRegistrationService
{
    private readonly IPolicyholderRepository policyholderRepository;
    private readonly IAccountRepository accountRepository;
    private readonly IRegistrationRepository registrationRepository;

    public RegistrationService(
        IPolicyholderRepository policyholderRepository,
        IAccountRepository accountRepository,
        IRegistrationRepository registrationRepository)
    {
        this.policyholderRepository = policyholderRepository;
        this.accountRepository = accountRepository;
        this.registrationRepository = registrationRepository;
    }

    public async Task<PolicyholderValidationResult> ValidatePolicyholderAsync(
        PolicyholderValidationRequest request)
    {
        var policyholder =
            await policyholderRepository.FindPolicyholderAsync(
                request.SSN,
                request.PolicyNumber,
                request.FirstName,
                request.LastName,
                request.DateOfBirth,
                request.ZipCode);

        if (policyholder == null)
{
    return PolicyholderValidationResult.NotFound;
}

return PolicyholderValidationResult.Validated;
    }

    public async Task<RegistrationResult> RegisterAsync(RegisterRequest request)
    {
        var policyholder =
            await policyholderRepository.FindPolicyholderAsync(
                request.PersonalInformation.SSN,
                request.PersonalInformation.PolicyNumber,
                request.PersonalInformation.FirstName,
                request.PersonalInformation.LastName,
                request.PersonalInformation.DateOfBirth,
                request.PersonalInformation.ZipCode);

        if (policyholder == null)
        {
            return RegistrationResult.PolicyholderNotFound;
        }

        var policyholderHasAccount =
            await accountRepository.PolicyholderHasAccountAsync(
                policyholder.Id);

        if (policyholderHasAccount)
        {
            return RegistrationResult.PolicyholderAlreadyRegistered;
        }

        var usernameExists =
            await accountRepository.UsernameExistsAsync(
                request.AccountInformation.Username);

        if (usernameExists)
        {
            return RegistrationResult.UsernameExists;
        }

        var account = new Account
        {
            Id = Guid.NewGuid(),
            PolicyholderId = policyholder.Id,
            Username = request.AccountInformation.Username,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(
                request.AccountInformation.Password)
        };

        var contact = new ContactInformation
        {
            Id = Guid.NewGuid(),
            PolicyholderId = policyholder.Id,
            Email = request.ContactInformation.Email,
            Phone = request.ContactInformation.Phone,
            StreetAddress = request.ContactInformation.StreetAddress,
            StreetAddressLine2 =
                request.ContactInformation.StreetAddressLine2,
            City = request.ContactInformation.City,
            State = request.ContactInformation.State,
            ZipCode = request.ContactInformation.ZipCode,
            Country = request.ContactInformation.Country
        };

        var securityInformation = new SecurityInformation
        {
            Id = Guid.NewGuid(),
            PolicyholderId = policyholder.Id,
            NicknameHash = BCrypt.Net.BCrypt.HashPassword(
                request.SecurityInformation.Nickname),
            ChildhoodHeroHash = BCrypt.Net.BCrypt.HashPassword(
                request.SecurityInformation.ChildhoodHero)
        };

        await registrationRepository.AddRegistrationAsync(
            account,
            contact,
            securityInformation);

        return RegistrationResult.Registered;
    }

}