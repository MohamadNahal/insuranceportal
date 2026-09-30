using InsurancePortalRegistration.Models;

namespace InsurancePortalRegistration.Repositories;

public interface ISecurityInformationRepository
{
    Task AddSecurityInformationAsync(SecurityInformation securityInformation);
}