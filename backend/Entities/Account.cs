using System.ComponentModel.DataAnnotations;

namespace InsurancePortalRegistration.Entities;

public class Account
{
    public Guid Id { get; set; }

    public Guid PolicyholderId { get; set; }

    [MaxLength(100)]
    public string Username { get; set; } = "";

    [MaxLength(255)]
    public string PasswordHash { get; set; } = "";

    public Policyholder? Policyholder { get; set; }
}
