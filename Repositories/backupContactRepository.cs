using System.Text.Json;
using InsurancePortalRegistration.Models;

namespace InsurancePortalRegistration.Repositories;

public class ContactRepository : IContactRepository
{
    private readonly string _filePath = "Data/contacts.json";

    public async Task AddContactAsync(ContactInformation contact)
    {
        var json = await File.ReadAllTextAsync(_filePath);

        var data = JsonSerializer.Deserialize<ContactData>(
            json,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }) ?? new ContactData();

        data.Contacts.Add(contact);

        var updatedJson = JsonSerializer.Serialize(
            data,
            new JsonSerializerOptions
            {
                WriteIndented = true
            });

        await File.WriteAllTextAsync(_filePath,updatedJson);
    }
}

public class ContactData
{
    public List<ContactInformation> Contacts { get; set; } = new();
}