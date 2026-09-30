namespace InsurancePortalRegistration.Models;

public class SecurityInformation
{
    public Guid Id { get; set; }

    public Guid PolicyholderId { get; set; }

    public string NicknameHash { get; set; } = "";

    public string ChildhoodHeroHash { get; set; } = "";
}