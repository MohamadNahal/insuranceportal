using InsurancePortalRegistration.DTOs;
using InsurancePortalRegistration.Exceptions;
using InsurancePortalRegistration.Models;
using InsurancePortalRegistration.Repositories;

namespace InsurancePortalRegistration.Services;

public class RegistrationService : IRegistrationService
{
    private readonly IPolicyholderRepository policyholderRepository;
    private readonly IAccountRepository accountRepository;
    private readonly IContactRepository contactRepository;
    private readonly ISecurityInformationRepository securityInformationRepository;
    private readonly ILogger<RegistrationService> logger;

    public RegistrationService(
        IPolicyholderRepository policyholderRepository,
        IAccountRepository accountRepository,
        IContactRepository contactRepository,
        ISecurityInformationRepository securityInformationRepository,
        ILogger<RegistrationService> logger)
    {
        this.policyholderRepository = policyholderRepository;
        this.accountRepository = accountRepository;
        this.contactRepository = contactRepository;
        this.securityInformationRepository = securityInformationRepository;
        this.logger = logger;
    }

    public async Task<string?> ValidatePolicyholderAsync(
        PolicyholderValidationRequest request)
    {
        try
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
                return null;
            }

            return "VALIDATED";
        }
        catch (Exception ex)
        {
            logger.LogError(
                ex,
                "Could not validate policyholder.");

            throw;
        }
    }

    public async Task<string?> RegisterAsync(
        RegisterRequest request)
    {
        try
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
                return null;
            }

            var policyholderHasAccount =
                await accountRepository.PolicyholderHasAccountAsync(
                    policyholder.Id);

            if (policyholderHasAccount)
            {
                return "POLICYHOLDER_ALREADY_REGISTERED";
            }

            var usernameExists =
                await accountRepository.UsernameExistsAsync(
                    request.AccountInformation.Username);

            if (usernameExists)
            {
                return "USERNAME_EXISTS";
            }

            var account = new Account
            {
                Id = Guid.NewGuid(),
                PolicyholderId = policyholder.Id,
                Username = request.AccountInformation.Username,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(
                    request.AccountInformation.Password)
            };

            await accountRepository.AddAccountAsync(account);

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

            await contactRepository.AddContactAsync(contact);

            var securityInformation = new SecurityInformation
            {
                Id = Guid.NewGuid(),
                PolicyholderId = policyholder.Id,
                NicknameHash = BCrypt.Net.BCrypt.HashPassword(
                    request.SecurityInformation.Nickname),
                ChildhoodHeroHash = BCrypt.Net.BCrypt.HashPassword(
                    request.SecurityInformation.ChildhoodHero)
            };

            await securityInformationRepository
                .AddSecurityInformationAsync(
                    securityInformation);

            return "REGISTERED";
        }
        catch (Exception ex)
        {
            logger.LogError(
                ex,
                "Could not register policyholder.");

            throw;
        }
    }

    public async Task<Account?> LoginAsync(
        LoginRequest request)
    {
        try
        {
            var account =
                await accountRepository.FindByUsernameAsync(
                    request.Username);

            if (account == null)
            {
                return null;
            }

            var passwordValid =
                BCrypt.Net.BCrypt.Verify(
                    request.Password,
                    account.PasswordHash);

            if (!passwordValid)
            {
                return null;
            }

            return account;
        }
        catch (Exception ex)
        {
            logger.LogError(
                ex,
                "Could not process login.");

            throw;
        }
    }
}