using InsurancePortalRegistration.Entities;

namespace InsurancePortalRegistration.Repositories;

public interface IRegistrationRepository
{
    Task AddRegistrationAsync(
        Account account,
        ContactInformation contact,
        SecurityInformation securityInformation);
}