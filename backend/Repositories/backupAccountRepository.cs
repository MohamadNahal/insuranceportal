using System.Text.Json;
using InsurancePortalRegistration.Models;

namespace InsurancePortalRegistration.Repositories;

public class AccountRepository : IAccountRepository
{
    private readonly string _filePath = "Data/accounts.jsonl";

    public async Task<bool> UsernameExistsAsync(string username)
    {
        if (!File.Exists(_filePath))
        {
            return false;
        }

        var lines = await File.ReadAllLinesAsync(_filePath);

        foreach (var line in lines)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                continue;
            }

            var account = JsonSerializer.Deserialize<Account>(line);

            if (account != null &&
                account.Username.Equals(username,StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
        }

        return false;
    }

    public async Task<bool> PolicyholderHasAccountAsync(Guid policyholderId)
    {
        if (!File.Exists(_filePath))
        {
            return false;
        }

        var lines = await File.ReadAllLinesAsync(_filePath);

        foreach (var line in lines)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                continue;
            }

            var account = JsonSerializer.Deserialize<Account>(line);

            if (account != null && account.PolicyholderId == policyholderId)
            {
                return true;
            }
        }

        return false;
    }

    public async Task AddAccountAsync(Account account)
    {
        var json = JsonSerializer.Serialize(account);

        var prefix =
            File.Exists(_filePath) &&
            new FileInfo(_filePath).Length > 0
                ? Environment.NewLine
                : "";

        await File.AppendAllTextAsync(_filePath,prefix + json);
    }

    public async Task<Account?> FindByUsernameAsync(
        string username)
    {
        if (!File.Exists(_filePath))
        {
            return null;
        }

        var lines = await File.ReadAllLinesAsync(_filePath);

        foreach (var line in lines)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                continue;
            }

            var account = JsonSerializer.Deserialize<Account>(line);

            if (account != null &&
                account.Username.Equals(
                    username,
                    StringComparison.OrdinalIgnoreCase))
            {
                return account;
            }
        }

        return null;
    }
}