using InsurancePortalRegistration.DTOs;
using InsurancePortalRegistration.Models;
using InsurancePortalRegistration.Repositories;
using InsurancePortalRegistration.Services;
using Microsoft.Extensions.Logging;
using Moq;
namespace InsurancePortalRegistration.Tests;

public class RegistrationServiceTests
{
    [Test]
    public async Task ValidatePolicyholderAsync_WhenPolicyholderExists_ReturnsValidated()
    {
        var policyholderRepository =
            new Mock<IPolicyholderRepository>();

        var accountRepository =
            new Mock<IAccountRepository>();

        var contactRepository =
            new Mock<IContactRepository>();

        var securityInformationRepository =
            new Mock<ISecurityInformationRepository>();
    
        var logger =
            new Mock<ILogger<RegistrationService>>();

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

        var service = new RegistrationService(
            policyholderRepository.Object,
            accountRepository.Object,
            contactRepository.Object,
            securityInformationRepository.Object,
            logger.Object);

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

        Assert.That(result, Is.EqualTo("VALIDATED"));
    }

    [Test]
    public async Task RegisterAsync_WithValidInformation_ReturnsRegistered()
    {
        var policyholderRepository =
            new Mock<IPolicyholderRepository>();

        var accountRepository =
            new Mock<IAccountRepository>();

        var contactRepository =
            new Mock<IContactRepository>();

        var securityInformationRepository =
            new Mock<ISecurityInformationRepository>();

        var logger =
            new Mock<ILogger<RegistrationService>>();

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

    accountRepository
        .Setup(repository =>
            repository.AddAccountAsync(
                It.IsAny<Account>()))
        .Returns(Task.CompletedTask);

    contactRepository
        .Setup(repository =>
            repository.AddContactAsync(
                It.IsAny<ContactInformation>()))
        .Returns(Task.CompletedTask);

    securityInformationRepository
        .Setup(repository =>
            repository.AddSecurityInformationAsync(
                It.IsAny<SecurityInformation>()))
        .Returns(Task.CompletedTask);

    var service = new RegistrationService(
        policyholderRepository.Object,
        accountRepository.Object,
        contactRepository.Object,
        securityInformationRepository.Object,
        logger.Object);

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

    Assert.That(result, Is.EqualTo("REGISTERED"));
}
[Test]
public async Task LoginAsync_WithValidCredentials_ReturnsAccount()
{
    var policyholderRepository =
        new Mock<IPolicyholderRepository>();

    var accountRepository =
        new Mock<IAccountRepository>();

    var contactRepository =
        new Mock<IContactRepository>();

    var securityInformationRepository =
        new Mock<ISecurityInformationRepository>();

    var logger =
        new Mock<ILogger<RegistrationService>>();

    var account = new Account
    {
        Id = Guid.NewGuid(),
        PolicyholderId = Guid.NewGuid(),
        Username = "johnsmith",
        PasswordHash = BCrypt.Net.BCrypt.HashPassword(
            "Password123!")
    };

    accountRepository
        .Setup(repository =>
            repository.FindByUsernameAsync("johnsmith"))
        .ReturnsAsync(account);

    var service = new RegistrationService(
        policyholderRepository.Object,
        accountRepository.Object,
        contactRepository.Object,
        securityInformationRepository.Object,
        logger.Object);

    var request = new LoginRequest
    {
        Username = "johnsmith",
        Password = "Password123!"
    };

    var result =
        await service.LoginAsync(request);

    Assert.That(result, Is.Not.Null);
    Assert.That(result!.Username, Is.EqualTo("johnsmith"));
}
}
