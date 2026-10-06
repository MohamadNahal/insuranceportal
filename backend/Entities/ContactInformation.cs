using System.ComponentModel.DataAnnotations;

namespace InsurancePortalRegistration.Entities;

public class ContactInformation
{
    public Guid Id { get; set; }

    public Guid PolicyholderId { get; set; }

    [MaxLength(255)]
    public string Email { get; set; } = "";

    [MaxLength(10)]
    public string Phone { get; set; } = "";

    [MaxLength(200)]
    public string StreetAddress { get; set; } = "";

    [MaxLength(200)]
    public string StreetAddressLine2 { get; set; } = "";

    [MaxLength(100)]
    public string City { get; set; } = "";

    [MaxLength(100)]
    public string State { get; set; } = "";

    [MaxLength(5)]
    public string ZipCode { get; set; } = "";

    [MaxLength(100)]
    public string Country { get; set; } = "";
}