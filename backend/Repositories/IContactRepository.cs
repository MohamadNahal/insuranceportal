using InsurancePortalRegistration.Models;

namespace InsurancePortalRegistration.Repositories;

public interface IContactRepository
{
    Task AddContactAsync(ContactInformation contact);
}