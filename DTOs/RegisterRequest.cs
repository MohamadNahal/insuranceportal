using System.ComponentModel.DataAnnotations;

namespace InsurancePortalRegistration.DTOs;

public class RegisterRequest
{
    public PolicyholderValidationRequest PersonalInformation { get; set; }
    = new();
    

    public AccountInformationRequest AccountInformation { get; set; }
        = new();

    public SecurityInformationRequest SecurityInformation { get; set; }
        = new();

    public ContactInformationRequest ContactInformation { get; set; }
        = new();
}


public class AccountInformationRequest : IValidatableObject
{
    [Required]
    [StringLength(18, MinimumLength = 6)]
    public string Username { get; set; } = "";

    [Required]
    [RegularExpression(@"^(?=.*[A-Z])(?=.*\d)(?=.*[^a-zA-Z0-9]).{8,16}$",
        ErrorMessage =
            "Password must be 8 to 16 characters and contain at least one uppercase letter, one number, and a special character.")]
    public string Password { get; set; } = "";

    [Required]
    public string ConfirmPassword { get; set; } = "";

    public IEnumerable<ValidationResult> Validate(
        ValidationContext validationContext)
    {
        if (Password != ConfirmPassword)
        {
            yield return new ValidationResult(
                "Password and confirm password must match.",
                new[] { nameof(ConfirmPassword) });
        }
    }
}

public class SecurityInformationRequest
{
    [Required]
    public string Nickname { get; set; } = "";

    [Required]
    public string ChildhoodHero { get; set; } = "";
}

public class ContactInformationRequest
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = "";

    [Required]
    public string Phone { get; set; } = "";

    public string StreetAddress { get; set; } = "";

    public string StreetAddressLine2 { get; set; } = "";

    [Required]
    public string City { get; set; } = "";

    [Required]
    public string State { get; set; } = "";

    [Required]
    [RegularExpression(@"^\d{5}$")]
    public string ZipCode { get; set; } = "";

    [Required]
    public string Country { get; set; } = "";
}