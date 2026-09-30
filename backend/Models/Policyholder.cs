namespace InsurancePortalRegistration.Models;

public class Policyholder
{
    public Guid Id { get; set; }

    public string SSN { get; set; } = "";

    public string PolicyNumber { get; set; } = "";

    public string FirstName { get; set; } = "";

    public string LastName { get; set; } = "";

    public string DateOfBirth { get; set; } = "";

    public string ZipCode { get; set; } = "";

    public string Email { get; set; } = "";

    public string Phone { get; set; } = "";
}