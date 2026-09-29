namespace InsurancePortalRegistration.Models;

public class ContactInformation
{
    public Guid Id { get; set; }

    public Guid PolicyholderId { get; set; }

    public string Email { get; set; } = "";

    public string Phone { get; set; } = "";

    public string StreetAddress { get; set; } = "";

    public string StreetAddressLine2 { get; set; } = "";

    public string City { get; set; } = "";

    public string State { get; set; } = "";

    public string ZipCode { get; set; } = "";

    public string Country { get; set; } = "";
}