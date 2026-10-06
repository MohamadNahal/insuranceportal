using InsurancePortalRegistration.Entities;

namespace InsurancePortalRegistration.Repositories;

public interface IContactRepository
{
    Task AddContactAsync(ContactInformation contact);
}