
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using InsurancePortalRegistration.Data;
using InsurancePortalRegistration.Entities;
using InsurancePortalRegistration.Exceptions;
using InsurancePortalRegistration.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using NUnit.Framework;

namespace InsurancePortalRegistration.Tests;

[TestFixture]
public class SqlAccountRepositoryTests
{
    [Test]
    public async Task UsernameExistsAsync_WhenUsernameExists_ReturnsTrue()
    {
        var (context, repository) = CreateRepository();

        await using (context)
        {
            context.Accounts.Add(CreateAccount("testuser"));
            await context.SaveChangesAsync();

            var result = await repository.UsernameExistsAsync("testuser");

            Assert.That(result, Is.True);
        }
    }

    [Test]
    public async Task UsernameExistsAsync_WhenUsernameDoesNotExist_ReturnsFalse()
    {
        var (context, repository) = CreateRepository();

        await using (context)
        {
            var result = await repository.UsernameExistsAsync("missinguser");

            Assert.That(result, Is.False);
        }
    }

    [Test]
    public async Task PolicyholderHasAccountAsync_WhenAccountExists_ReturnsTrue()
    {
        var (context, repository) = CreateRepository();

        await using (context)
        {
            var policyholderId = Guid.NewGuid();
            context.Accounts.Add(CreateAccount("testuser", policyholderId));
            await context.SaveChangesAsync();

            var result = await repository.PolicyholderHasAccountAsync(policyholderId);

            Assert.That(result, Is.True);
        }
    }

    [Test]
    public async Task PolicyholderHasAccountAsync_WhenAccountDoesNotExist_ReturnsFalse()
    {
        var (context, repository) = CreateRepository();

        await using (context)
        {
            var result = await repository.PolicyholderHasAccountAsync(Guid.NewGuid());

            Assert.That(result, Is.False);
        }
    }

    [Test]
    public async Task AddAccountAsync_WhenAccountIsValid_SavesAccount()
    {
        var (context, repository) = CreateRepository();

        await using (context)
        {
            var account = CreateAccount("newuser");

            await repository.AddAccountAsync(account);

            var savedAccount = await context.Accounts
                .SingleOrDefaultAsync(x => x.Id == account.Id);

            Assert.That(savedAccount, Is.Not.Null);
            Assert.That(savedAccount!.Username, Is.EqualTo("newuser"));
            Assert.That(savedAccount.PolicyholderId, Is.EqualTo(account.PolicyholderId));
            Assert.That(savedAccount.PasswordHash, Is.EqualTo(account.PasswordHash));
        }
    }

    [Test]
    public async Task FindByUsernameAsync_WhenAccountExists_ReturnsAccount()
    {
        var (context, repository) = CreateRepository();

        await using (context)
        {
            var account = CreateAccount("findme");
            context.Accounts.Add(account);
            await context.SaveChangesAsync();

            var result = await repository.FindByUsernameAsync("findme");

            Assert.That(result, Is.Not.Null);
            Assert.That(result!.Id, Is.EqualTo(account.Id));
            Assert.That(result.Username, Is.EqualTo("findme"));
        }
    }

    [Test]
    public async Task FindByUsernameAsync_WhenAccountDoesNotExist_ReturnsNull()
    {
        var (context, repository) = CreateRepository();

        await using (context)
        {
            var result = await repository.FindByUsernameAsync("missinguser");

            Assert.That(result, Is.Null);
        }
    }

    [Test]
    public void AddAccountAsync_WhenSavingFails_ThrowsCouldNotAddAccountException()
    {
        var options = new DbContextOptionsBuilder<InsurancePortalDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        using var context = new FailingSaveChangesDbContext(options);

        var repository = new SqlAccountRepository(
            context,
            NullLogger<SqlAccountRepository>.Instance);

        var account = CreateAccount("failureuser");

        Assert.ThrowsAsync<CouldNotAddAccountException>(
            async () => await repository.AddAccountAsync(account));
    }

    private static (InsurancePortalDbContext Context, SqlAccountRepository Repository)
        CreateRepository()
    {
        var options = new DbContextOptionsBuilder<InsurancePortalDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        var context = new InsurancePortalDbContext(options);

        var repository = new SqlAccountRepository(
            context,
            NullLogger<SqlAccountRepository>.Instance);

        return (context, repository);
    }

    private static Account CreateAccount(
        string username,
        Guid? policyholderId = null)
    {
        return new Account
        {
            Id = Guid.NewGuid(),
            PolicyholderId = policyholderId ?? Guid.NewGuid(),
            Username = username,
            PasswordHash = "test-password-hash"
        };
    }

    private sealed class FailingSaveChangesDbContext
        : InsurancePortalDbContext
    {
        public FailingSaveChangesDbContext(
            DbContextOptions<InsurancePortalDbContext> options)
            : base(options)
        {
        }

        public override Task<int> SaveChangesAsync(
            CancellationToken cancellationToken = default)
        {
            throw new InvalidOperationException(
                "Simulated database save failure.");
        }
    }
}
