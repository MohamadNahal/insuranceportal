
using System.ComponentModel.DataAnnotations;

namespace InsurancePortalRegistration.Entities;

public class Policyholder
{
    public Guid Id { get; set; }

    [MaxLength(4)]
    public string SSN { get; set; } = "";

    [MaxLength(9)]
    public string PolicyNumber { get; set; } = "";

    [MaxLength(100)]
    public string FirstName { get; set; } = "";

    [MaxLength(100)]
    public string LastName { get; set; } = "";

    [MaxLength(10)]
    public string DateOfBirth { get; set; } = "";

    [MaxLength(5)]
    public string ZipCode { get; set; } = "";

    [MaxLength(255)]
    public string Email { get; set; } = "";

    [MaxLength(10)]
    public string Phone { get; set; } = "";

    public Account? Account { get; set; }

    public ContactInformation? ContactInformation { get; set; }

    public SecurityInformation? SecurityInformation { get; set; }
}