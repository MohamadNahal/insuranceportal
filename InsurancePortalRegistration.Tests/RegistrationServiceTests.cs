using InsurancePortalRegistration.DTOs;
using InsurancePortalRegistration.Entities;
using InsurancePortalRegistration.Repositories;
using InsurancePortalRegistration.Services;
using Microsoft.Extensions.Logging;
using Moq;

namespace InsurancePortalRegistration.Tests;

public class RegistrationServiceTests
{
    private Mock<IPolicyholderRepository> policyholderRepository = null!;
    private Mock<IAccountRepository> accountRepository = null!;
    private Mock<IRegistrationRepository> registrationRepository = null!;

    private RegistrationService service = null!;
    private AuthService authService = null!;

    [SetUp]
    public void SetUp()
    {
        policyholderRepository =
            new Mock<IPolicyholderRepository>();

        accountRepository =
            new Mock<IAccountRepository>();

        registrationRepository =
            new Mock<IRegistrationRepository>();

        service = new RegistrationService(
            policyholderRepository.Object,
            accountRepository.Object,
            registrationRepository.Object);
        authService = new AuthService(
            accountRepository.Object);
    }

    [Test]
    public async Task ValidatePolicyholderAsync_WhenPolicyholderExists_ReturnsValidated()
    {
        var policyholder = new Policyholder
        {
            Id = Guid.NewGuid(),
            SSN = "1234",
            PolicyNumber = "AB1234567",
            FirstName = "John",
            LastName = "Smith",
            DateOfBirth = "01/01/1990",
            ZipCode = "12345"
        };

        policyholderRepository
            .Setup(repository =>
                repository.FindPolicyholderAsync(
                    "1234",
                    "AB1234567",
                    "John",
                    "Smith",
                    "01/01/1990",
                    "12345"))
            .ReturnsAsync(policyholder);

        var request = new PolicyholderValidationRequest
        {
            SSN = "1234",
            PolicyNumber = "AB1234567",
            FirstName = "John",
            LastName = "Smith",
            DateOfBirth = "01/01/1990",
            ZipCode = "12345"
        };

        var result =
            await service.ValidatePolicyholderAsync(request);

        Assert.That(
    result,
    Is.EqualTo(PolicyholderValidationResult.Validated));
    }

    [Test]
    public async Task RegisterAsync_WithValidInformation_ReturnsRegistered()
    {
        var policyholder = new Policyholder
        {
            Id = Guid.NewGuid(),
            SSN = "1234",
            PolicyNumber = "AB1234567",
            FirstName = "John",
            LastName = "Smith",
            DateOfBirth = "01/01/1990",
            ZipCode = "12345"
        };

        policyholderRepository
            .Setup(repository =>
                repository.FindPolicyholderAsync(
                    "1234",
                    "AB1234567",
                    "John",
                    "Smith",
                    "01/01/1990",
                    "12345"))
            .ReturnsAsync(policyholder);

        accountRepository
            .Setup(repository =>
                repository.PolicyholderHasAccountAsync(
                    policyholder.Id))
            .ReturnsAsync(false);

        accountRepository
            .Setup(repository =>
                repository.UsernameExistsAsync("johnsmith"))
            .ReturnsAsync(false);

        registrationRepository
            .Setup(repository =>
                repository.AddRegistrationAsync(
                    It.IsAny<Account>(),
                    It.IsAny<ContactInformation>(),
                    It.IsAny<SecurityInformation>()))
            .Returns(Task.CompletedTask);

        var request = new RegisterRequest
        {
            PersonalInformation = new PolicyholderValidationRequest
            {
                SSN = "1234",
                PolicyNumber = "AB1234567",
                FirstName = "John",
                LastName = "Smith",
                DateOfBirth = "01/01/1990",
                ZipCode = "12345"
            },

            AccountInformation = new AccountInformationRequest
            {
                Username = "johnsmith",
                Password = "Password123!",
                ConfirmPassword = "Password123!"
            },

            SecurityInformation = new SecurityInformationRequest
            {
                Nickname = "Johnny",
                ChildhoodHero = "Superman"
            },

            ContactInformation = new ContactInformationRequest
            {
                Email = "john@example.com",
                Phone = "1234567890",
                StreetAddress = "123 Main Street",
                StreetAddressLine2 = "",
                City = "Boston",
                State = "Massachusetts",
                ZipCode = "12345",
                Country = "USA"
            }
        };

        var result =
            await service.RegisterAsync(request);

        Assert.That(
    result,
    Is.EqualTo(RegistrationResult.Registered));

        registrationRepository.Verify(
            repository =>
                repository.AddRegistrationAsync(
                    It.IsAny<Account>(),
                    It.IsAny<ContactInformation>(),
                    It.IsAny<SecurityInformation>()),
            Times.Once);
    }

    [Test]
    public async Task LoginAsync_WithValidCredentials_ReturnsAccount()
    {
        var account = new Account
        {
            Id = Guid.NewGuid(),
            PolicyholderId = Guid.NewGuid(),
            Username = "johnsmith",
            PasswordHash =
                BCrypt.Net.BCrypt.HashPassword(
                    "Password123!")
        };

        accountRepository
            .Setup(repository =>
                repository.FindByUsernameAsync("johnsmith"))
            .ReturnsAsync(account);

        var request = new LoginRequest
        {
            Username = "johnsmith",
            Password = "Password123!"
        };

        var result =
            await authService.LoginAsync(request);

        Assert.That(result, Is.Not.Null);
        Assert.That(result!.Username, Is.EqualTo("johnsmith"));
    }

    [Test]
    public async Task RegisterAsync_WhenUsernameExists_SavesNothing()
    {
        var policyholder = new Policyholder
        {
            Id = Guid.NewGuid(),
            SSN = "1234",
            PolicyNumber = "AB1234567",
            FirstName = "John",
            LastName = "Smith",
            DateOfBirth = "01/01/1990",
            ZipCode = "12345"
        };

        policyholderRepository
            .Setup(repository =>
                repository.FindPolicyholderAsync(
                    "1234",
                    "AB1234567",
                    "John",
                    "Smith",
                    "01/01/1990",
                    "12345"))
            .ReturnsAsync(policyholder);

        accountRepository
            .Setup(repository =>
                repository.PolicyholderHasAccountAsync(
                    policyholder.Id))
            .ReturnsAsync(false);

        accountRepository
            .Setup(repository =>
                repository.UsernameExistsAsync("johnsmith"))
            .ReturnsAsync(true);

        var request = new RegisterRequest
        {
            PersonalInformation = new PolicyholderValidationRequest
            {
                SSN = "1234",
                PolicyNumber = "AB1234567",
                FirstName = "John",
                LastName = "Smith",
                DateOfBirth = "01/01/1990",
                ZipCode = "12345"
            },

            AccountInformation = new AccountInformationRequest
            {
                Username = "johnsmith",
                Password = "Password123!",
                ConfirmPassword = "Password123!"
            },

            SecurityInformation = new SecurityInformationRequest
            {
                Nickname = "Johnny",
                ChildhoodHero = "Superman"
            },

            ContactInformation = new ContactInformationRequest
            {
                Email = "john@example.com",
                Phone = "1234567890",
                StreetAddress = "123 Main Street",
                StreetAddressLine2 = "",
                City = "Boston",
                State = "Massachusetts",
                ZipCode = "12345",
                Country = "USA"
            }
        };

        var result =
            await service.RegisterAsync(request);

        Assert.That(
    result,
    Is.EqualTo(RegistrationResult.UsernameExists));

        registrationRepository.Verify(
            repository =>
                repository.AddRegistrationAsync(
                    It.IsAny<Account>(),
                    It.IsAny<ContactInformation>(),
                    It.IsAny<SecurityInformation>()),
            Times.Never);
    }

    [Test]
    public async Task RegisterAsync_WithValidInformation_SavesHashedPassword()
    {
        var policyholder = new Policyholder
        {
            Id = Guid.NewGuid(),
            SSN = "1234",
            PolicyNumber = "AB1234567",
            FirstName = "John",
            LastName = "Smith",
            DateOfBirth = "01/01/1990",
            ZipCode = "12345"
        };

        policyholderRepository
            .Setup(repository =>
                repository.FindPolicyholderAsync(
                    "1234",
                    "AB1234567",
                    "John",
                    "Smith",
                    "01/01/1990",
                    "12345"))
            .ReturnsAsync(policyholder);

        accountRepository
            .Setup(repository =>
                repository.PolicyholderHasAccountAsync(
                    policyholder.Id))
            .ReturnsAsync(false);

        accountRepository
            .Setup(repository =>
                repository.UsernameExistsAsync("johnsmith"))
            .ReturnsAsync(false);

        Account? savedAccount = null;

        registrationRepository
            .Setup(repository =>
                repository.AddRegistrationAsync(
                    It.IsAny<Account>(),
                    It.IsAny<ContactInformation>(),
                    It.IsAny<SecurityInformation>()))
            .Callback<Account, ContactInformation, SecurityInformation>(
                (account, contact, securityInformation) =>
                {
                    savedAccount = account;
                })
            .Returns(Task.CompletedTask);

        var request = new RegisterRequest
        {
            PersonalInformation = new PolicyholderValidationRequest
            {
                SSN = "1234",
                PolicyNumber = "AB1234567",
                FirstName = "John",
                LastName = "Smith",
                DateOfBirth = "01/01/1990",
                ZipCode = "12345"
            },

            AccountInformation = new AccountInformationRequest
            {
                Username = "johnsmith",
                Password = "Password123!",
                ConfirmPassword = "Password123!"
            },

            SecurityInformation = new SecurityInformationRequest
            {
                Nickname = "Johnny",
                ChildhoodHero = "Superman"
            },

            ContactInformation = new ContactInformationRequest
            {
                Email = "john@example.com",
                Phone = "1234567890",
                StreetAddress = "123 Main Street",
                StreetAddressLine2 = "",
                City = "Boston",
                State = "Massachusetts",
                ZipCode = "12345",
                Country = "USA"
            }
        };

        await service.RegisterAsync(request);

        Assert.That(savedAccount, Is.Not.Null);

        Assert.That(
            BCrypt.Net.BCrypt.Verify(
                "Password123!",
                savedAccount!.PasswordHash),
            Is.True);
    }

    [Test]
    public async Task LoginAsync_WithWrongPassword_ReturnsNull()
    {
        var account = new Account
        {
            Id = Guid.NewGuid(),
            PolicyholderId = Guid.NewGuid(),
            Username = "johnsmith",
            PasswordHash =
                BCrypt.Net.BCrypt.HashPassword(
                    "Password123!")
        };

        accountRepository
            .Setup(repository =>
                repository.FindByUsernameAsync("johnsmith"))
            .ReturnsAsync(account);

        var request = new LoginRequest
        {
            Username = "johnsmith",
            Password = "WrongPassword123!"
        };

        var result =
            await authService.LoginAsync(request);

        Assert.That(result, Is.Null);
    }

    [Test]
    public async Task LoginAsync_WhenUsernameDoesNotExist_ReturnsNull()
    {
        accountRepository
            .Setup(repository =>
                repository.FindByUsernameAsync("unknownuser"))
            .ReturnsAsync((Account?)null);

        var request = new LoginRequest
        {
            Username = "unknownuser",
            Password = "Password123!"
        };

        var result =
            await authService.LoginAsync(request);

        Assert.That(result, Is.Null);
    }

    [Test]
    public async Task ValidatePolicyholderAsync_WhenPolicyholderDoesNotExist_ReturnsNull()
    {
        policyholderRepository
            .Setup(repository =>
                repository.FindPolicyholderAsync(
                    "9999",
                    "ZZ9999999",
                    "Unknown",
                    "Person",
                    "01/01/1990",
                    "99999"))
            .ReturnsAsync((Policyholder?)null);

        var request = new PolicyholderValidationRequest
        {
            SSN = "9999",
            PolicyNumber = "ZZ9999999",
            FirstName = "Unknown",
            LastName = "Person",
            DateOfBirth = "01/01/1990",
            ZipCode = "99999"
        };

        var result =
            await service.ValidatePolicyholderAsync(request);

        Assert.That(
            result,
            Is.EqualTo(PolicyholderValidationResult.NotFound));
    }

    [Test]
    public async Task RegisterAsync_WhenPolicyholderAlreadyHasAccount_ReturnsAlreadyRegistered()
    {
        var policyholder = new Policyholder
        {
            Id = Guid.NewGuid(),
            SSN = "1234",
            PolicyNumber = "AB1234567",
            FirstName = "John",
            LastName = "Smith",
            DateOfBirth = "01/01/1990",
            ZipCode = "12345"
        };

        policyholderRepository
            .Setup(repository =>
                repository.FindPolicyholderAsync(
                    "1234",
                    "AB1234567",
                    "John",
                    "Smith",
                    "01/01/1990",
                    "12345"))
            .ReturnsAsync(policyholder);

        accountRepository
            .Setup(repository =>
                repository.PolicyholderHasAccountAsync(
                    policyholder.Id))
            .ReturnsAsync(true);

        var request = new RegisterRequest
        {
            PersonalInformation = new PolicyholderValidationRequest
            {
                SSN = "1234",
                PolicyNumber = "AB1234567",
                FirstName = "John",
                LastName = "Smith",
                DateOfBirth = "01/01/1990",
                ZipCode = "12345"
            },

            AccountInformation = new AccountInformationRequest
            {
                Username = "johnsmith",
                Password = "Password123!",
                ConfirmPassword = "Password123!"
            },

            SecurityInformation = new SecurityInformationRequest
            {
                Nickname = "Johnny",
                ChildhoodHero = "Superman"
            },

            ContactInformation = new ContactInformationRequest
            {
                Email = "john@example.com",
                Phone = "1234567890",
                StreetAddress = "123 Main Street",
                StreetAddressLine2 = "",
                City = "Boston",
                State = "Massachusetts",
                ZipCode = "12345",
                Country = "USA"
            }
        };

        var result =
            await service.RegisterAsync(request);

        Assert.That(
            result,
            Is.EqualTo(RegistrationResult.PolicyholderAlreadyRegistered));

        registrationRepository.Verify(
            repository =>
                repository.AddRegistrationAsync(
                    It.IsAny<Account>(),
                    It.IsAny<ContactInformation>(),
                    It.IsAny<SecurityInformation>()),
            Times.Never);
    }
    [Test]
public async Task RegisterAsync_WhenPolicyholderDoesNotExist_ReturnsNotFound()
{
    policyholderRepository
        .Setup(repository =>
            repository.FindPolicyholderAsync(
                "9999",
                "ZZ9999999",
                "Unknown",
                "Person",
                "01/01/1990",
                "99999"))
        .ReturnsAsync((Policyholder?)null);

    var request = new RegisterRequest
    {
        PersonalInformation = new PolicyholderValidationRequest
        {
            SSN = "9999",
            PolicyNumber = "ZZ9999999",
            FirstName = "Unknown",
            LastName = "Person",
            DateOfBirth = "01/01/1990",
            ZipCode = "99999"
        },
        AccountInformation = new AccountInformationRequest
        {
            Username = "unknownuser",
            Password = "Password123!",
            ConfirmPassword = "Password123!"
        },
        SecurityInformation = new SecurityInformationRequest
        {
            Nickname = "Unknown",
            ChildhoodHero = "Superman"
        },
        ContactInformation = new ContactInformationRequest
        {
            Email = "unknown@example.com",
            Phone = "1234567890",
            StreetAddress = "123 Main Street",
            StreetAddressLine2 = "",
            City = "Boston",
            State = "Massachusetts",
            ZipCode = "12345",
            Country = "USA"
        }
    };

    var result =
        await service.RegisterAsync(request);

    Assert.That(
        result,
        Is.EqualTo(RegistrationResult.PolicyholderNotFound));

    registrationRepository.Verify(
        repository =>
            repository.AddRegistrationAsync(
                It.IsAny<Account>(),
                It.IsAny<ContactInformation>(),
                It.IsAny<SecurityInformation>()),
        Times.Never);
}
}