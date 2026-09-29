using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace InsurancePortalRegistration.DTOs;

public class PolicyholderValidationRequest : IValidatableObject
{
    [Required(ErrorMessage = "SSN is required.")]
    [RegularExpression(@"^\d{4}$",ErrorMessage = "SSN must contain exactly 4 digits.")]
    public string SSN { get; set; } = "";

    [Required(ErrorMessage = "Policy number is required.")]
    [RegularExpression(@"^[A-Za-z]{2}\d{7}$",ErrorMessage = "Enter a valid policy number.")]
    public string PolicyNumber { get; set; } = "";

    [Required(ErrorMessage = "First name is required.")]
    public string FirstName { get; set; } = "";

    [Required(ErrorMessage = "Last name is required.")]
    public string LastName { get; set; } = "";

    [Required(ErrorMessage = "Date of birth is required.")]
    public string DateOfBirth { get; set; } = "";

    [Required(ErrorMessage = "ZIP code is required.")]
    [RegularExpression(@"^\d{5}$",ErrorMessage = "ZIP code must contain exactly 5 digits.")]
    public string ZipCode { get; set; } = "";

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (!DateTime.TryParseExact(
                DateOfBirth,
                "MM/dd/yyyy",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out var dob))
        {
            yield return new ValidationResult(
                "Enter a valid DOB.",
                new[] { nameof(DateOfBirth) });

            yield break;
        }

        if (dob > DateTime.Today)
        {
            yield return new ValidationResult(
                "Enter a valid DOB.",
                new[] { nameof(DateOfBirth) });
        }
    }
}