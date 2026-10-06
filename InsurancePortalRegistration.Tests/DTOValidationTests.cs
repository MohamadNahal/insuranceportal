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

        Assert.That(results, Is.Not.Empty);
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

        Assert.That(results, Is.Not.Empty);
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

    private static List<ValidationResult> Validate(object model)
    {
        var results = new List<ValidationResult>();
        var context = new ValidationContext(model);

        Validator.TryValidateObject(
            model,
            context,
            results,
            true);

        if (model is IValidatableObject validatable)
        {
            results.AddRange(
                validatable.Validate(context));
        }

        return results;
    }
}