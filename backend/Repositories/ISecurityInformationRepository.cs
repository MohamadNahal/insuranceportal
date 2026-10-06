using InsurancePortalRegistration.Entities;

namespace InsurancePortalRegistration.Repositories;

public interface ISecurityInformationRepository
{
    Task AddSecurityInformationAsync(SecurityInformation securityInformation);
}