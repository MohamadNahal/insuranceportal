using System.Text.Json;
using InsurancePortalRegistration.Models;

namespace InsurancePortalRegistration.Repositories;

public class SecurityInformationRepository : ISecurityInformationRepository
{
    private readonly string _filePath ="Data/securityInformation.json";

    public async Task AddSecurityInformationAsync(SecurityInformation securityInformation)
    {
        var json = await File.ReadAllTextAsync(_filePath);

        var data = JsonSerializer.Deserialize<SecurityInformationData>(
            json,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }) ?? new SecurityInformationData();

        data.SecurityInformation.Add(securityInformation);

        var updatedJson = JsonSerializer.Serialize(
            data,
            new JsonSerializerOptions
            {
                WriteIndented = true
            });

        await File.WriteAllTextAsync(_filePath,updatedJson);
    }
}

    public class SecurityInformationData
    {
    public List<SecurityInformation> SecurityInformation { get; set; }
        = new();
    }