using System.ComponentModel.DataAnnotations;

namespace InsurancePortalRegistration.Entities;

public class SecurityInformation
{
    public Guid Id { get; set; }

    public Guid PolicyholderId { get; set; }

    [MaxLength(255)]
    public string NicknameHash { get; set; } = "";

    [MaxLength(255)]
    public string ChildhoodHeroHash { get; set; } = "";
}