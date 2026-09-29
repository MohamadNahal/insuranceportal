namespace InsurancePortalRegistration.Models;

public class Account
{
    public Guid Id { get; set; }

    public Guid PolicyholderId { get; set; }

    public string Username { get; set; } = "";

    public string PasswordHash { get; set; } = "";
}