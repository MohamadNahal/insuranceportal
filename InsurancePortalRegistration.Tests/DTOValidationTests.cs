using System.ComponentModel.DataAnnotations;
using InsurancePortalRegistration.DTOs;

namespace InsurancePortalRegistration.Tests;

public class DTOValidationTests
{
    [Test]
    public void PolicyholderValidationRequest_WithInvalidSsn_IsInvalid()
    {
        var request = new PolicyholderValidationRequest
        {
            SSN = "12",
            PolicyNumber = "AB1234567",
            FirstName = "John",
            LastName = "Smith",
            DateOfBirth = "01/01/1990",
            ZipCode = "12345"
        };

        var results = Validate(request);

        AssertHasErrorFor(results, nameof(request.SSN));
    }

    [Test]
    public void PolicyholderValidationRequest_WithInvalidPolicyNumber_IsInvalid()
    {
        var request = new PolicyholderValidationRequest
        {
            SSN = "1234",
            PolicyNumber = "123456789",
            FirstName = "John",
            LastName = "Smith",
            DateOfBirth = "01/01/1990",
            ZipCode = "12345"
        };

        var results = Validate(request);

        AssertHasErrorFor(results, nameof(request.PolicyNumber));
    }

    [Test]
    public void PolicyholderValidationRequest_WithFutureDob_IsInvalid()
    {
        var request = new PolicyholderValidationRequest
        {
            SSN = "1234",
            PolicyNumber = "AB1234567",
            FirstName = "John",
            LastName = "Smith",
            DateOfBirth = "12/31/2999",
            ZipCode = "12345"
        };

        var results = Validate(request);

        Assert.That(results, Is.Not.Empty);
    }

    [Test]
    public void ContactInformationRequest_WithInvalidZip_IsInvalid()
    {
        var request = new ContactInformationRequest
        {
            Email = "john@example.com",
            Phone = "1234567890",
            StreetAddress = "123 Main Street",
            City = "Boston",
            State = "Massachusetts",
            ZipCode = "123",
            Country = "USA"
        };

        var results = Validate(request);

        Assert.That(results, Is.Not.Empty);
    }

    [Test]
    public void AccountInformationRequest_WithMismatchedPasswords_IsInvalid()
    {
        var request = new AccountInformationRequest
        {
            Username = "johnsmith",
            Password = "Password123!",
            ConfirmPassword = "Different123!"
        };

        var results = Validate(request);

        Assert.That(results, Is.Not.Empty);
    }
    [Test]
    public void AccountInformationRequest_WithWhitespaceOnlyUsername_IsInvalid()
    {
        var request = new AccountInformationRequest
        {
            Username = "       ",
            Password = "Password123!",
            ConfirmPassword = "Password123!"
        };

        var results = Validate(request);

        Assert.That(results, Is.Not.Empty);
    }
    [Test]
    public void ContactInformationRequest_WithEmailTooLong_IsInvalid()
    {
        var request = ValidContactRequest();
        request.Email = new string('a', 247) + "@test.com";

        Assert.That(Validate(request), Is.Not.Empty);
    }

    [Test]
    public void ContactInformationRequest_WithPhoneTooLong_IsInvalid()
    {
        var request = ValidContactRequest();
        request.Phone = "12345678901";

        Assert.That(Validate(request), Is.Not.Empty);
    }

    [Test]
    public void ContactInformationRequest_WithStreetAddressTooLong_IsInvalid()
    {
        var request = ValidContactRequest();
        request.StreetAddress = new string('a', 201);

        Assert.That(Validate(request), Is.Not.Empty);
    }

    [Test]
    public void ContactInformationRequest_WithCityTooLong_IsInvalid()
    {
        var request = ValidContactRequest();
        request.City = new string('a', 101);

        Assert.That(Validate(request), Is.Not.Empty);
    }

    [Test]
    public void ContactInformationRequest_WithCountryTooLong_IsInvalid()
    {
        var request = ValidContactRequest();
        request.Country = new string('a', 101);

        Assert.That(Validate(request), Is.Not.Empty);
    }

    [Test]
    public void ContactInformationRequest_WithValidData_IsValid()
    {
        var request = ValidContactRequest();

        Assert.That(Validate(request), Is.Empty);
    }

    private static ContactInformationRequest ValidContactRequest()
    {
        return new ContactInformationRequest
        {
            Email = "john@example.com",
            Phone = "1234567890",
            StreetAddress = "123 Main Street",
            StreetAddressLine2 = "",
            City = "Boston",
            State = "Massachusetts",
            ZipCode = "12345",
            Country = "USA"
        };
    }
    private static List<ValidationResult> Validate(object model)
    {
        var results = new List<ValidationResult>();
        var context = new ValidationContext(model);

        Validator.TryValidateObject(
            model,
            context,
            results,
            validateAllProperties: true);

        return results;
    }
    private static void AssertHasErrorFor(
        IEnumerable<ValidationResult> results,
        string propertyName)
    {
        Assert.That(
            results.Any(result =>
                result.MemberNames.Contains(propertyName)),
            Is.True,
            $"Expected a validation error for {propertyName}.");
    }

    
}