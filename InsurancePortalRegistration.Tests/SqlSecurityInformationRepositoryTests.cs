
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
public class SqlSecurityInformationRepositoryTests
{
    [Test]
    public async Task AddSecurityInformationAsync_WithValidInformation_SavesToDatabase()
    {
        var options = new DbContextOptionsBuilder<InsurancePortalDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        await using var context = new InsurancePortalDbContext(options);
        var repository = new SqlSecurityInformationRepository(
            context,
            NullLogger<SqlSecurityInformationRepository>.Instance);
        var policyholderId = Guid.NewGuid();
        var securityInformation = new SecurityInformation
        {
            Id = Guid.NewGuid(),
            PolicyholderId = policyholderId,
            NicknameHash = "test-nickname-hash",
            ChildhoodHeroHash = "test-hero-hash"
        };
        await repository.AddSecurityInformationAsync(securityInformation);
        var savedInformation = await context.SecurityInformation
            .SingleOrDefaultAsync(x => x.Id == securityInformation.Id);
        Assert.That(savedInformation, Is.Not.Null);
        Assert.That(savedInformation!.PolicyholderId, Is.EqualTo(policyholderId));
        Assert.That(savedInformation.NicknameHash, Is.EqualTo("test-nickname-hash"));
        Assert.That(savedInformation.ChildhoodHeroHash, Is.EqualTo("test-hero-hash"));
    }

    [Test]
    public void AddSecurityInformationAsync_WhenSavingFails_ThrowsCouldNotAddSecurityInformationException()
    {
        var options = new DbContextOptionsBuilder<InsurancePortalDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        using var context = new FailingSaveChangesDbContext(options);
        var repository = new SqlSecurityInformationRepository(
            context,
            NullLogger<SqlSecurityInformationRepository>.Instance);
        var securityInformation = new SecurityInformation
        {
            Id = Guid.NewGuid(),
            PolicyholderId = Guid.NewGuid(),
            NicknameHash = "test-nickname-hash",
            ChildhoodHeroHash = "test-hero-hash"
        };

        Assert.ThrowsAsync<CouldNotAddSecurityInformationException>(
            async () => await repository.AddSecurityInformationAsync(
                securityInformation));
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
