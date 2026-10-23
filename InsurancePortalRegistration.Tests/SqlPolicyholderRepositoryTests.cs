using InsurancePortalRegistration.Data;
using InsurancePortalRegistration.Entities;
using InsurancePortalRegistration.Exceptions;
using InsurancePortalRegistration.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using NUnit.Framework;

namespace InsurancePortalRegistration.Tests;

[TestFixture]
public class SqlPolicyholderRepositoryTests
{
    private InsurancePortalDbContext context = null!;
    private SqlPolicyholderRepository repository = null!;

    [SetUp]
    public void SetUp()
    {
        var options = new DbContextOptionsBuilder<InsurancePortalDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        context = new InsurancePortalDbContext(options);

        repository = new SqlPolicyholderRepository(
            context,
            NullLogger<SqlPolicyholderRepository>.Instance);
    }

    [TearDown]
    public async Task TearDown()
    {
        await context.DisposeAsync();
    }

    [Test]
    public async Task FindPolicyholderAsync_WhenAllFieldsMatch_ReturnsPolicyholder()
    {
        var policyholder = CreatePolicyholder();

        await context.Policyholders.AddAsync(policyholder);
        await context.SaveChangesAsync();
        var result = await repository.FindPolicyholderAsync(
            policyholder.SSN,
            policyholder.PolicyNumber,
            policyholder.FirstName,
            policyholder.LastName,
            policyholder.DateOfBirth,
            policyholder.ZipCode);

        Assert.That(result, Is.Not.Null);
        Assert.That(result!.Id, Is.EqualTo(policyholder.Id));
    }

    [Test]
    public async Task FindPolicyholderAsync_WhenFieldsDoNotMatch_ReturnsNull()
    {
        var policyholder = CreatePolicyholder();

        await context.Policyholders.AddAsync(policyholder);
        await context.SaveChangesAsync();
        var result = await repository.FindPolicyholderAsync(
            policyholder.SSN,
            policyholder.PolicyNumber,
            policyholder.FirstName,
            policyholder.LastName,
            policyholder.DateOfBirth,
            "DIFFERENT-ZIP");
        Assert.That(result, Is.Null);
    }

    [Test]
    public async Task FindPolicyholderByIdAsync_WhenIdExists_ReturnsPolicyholder()
    {
        var policyholder = CreatePolicyholder();

        await context.Policyholders.AddAsync(policyholder);
        await context.SaveChangesAsync();

        var result = await repository.FindPolicyholderByIdAsync(
            policyholder.Id);

        Assert.That(result, Is.Not.Null);
        Assert.That(result!.Id, Is.EqualTo(policyholder.Id));
    }

    [Test]
    public async Task FindPolicyholderByIdAsync_WhenIdDoesNotExist_ReturnsNull()
    {
        var result = await repository.FindPolicyholderByIdAsync(
            Guid.NewGuid());
        Assert.That(result, Is.Null);
    }

    [Test]
    public void FindPolicyholderAsync_WhenDatabaseQueryFails_ThrowsCouldNotFindPolicyholderException()
    {
        var options = new DbContextOptionsBuilder<InsurancePortalDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        using var failingContext =
            new FailingPolicyholderDbContext(options);

        var failingRepository = new SqlPolicyholderRepository(
            failingContext,
            NullLogger<SqlPolicyholderRepository>.Instance);
        Assert.ThrowsAsync<CouldNotFindPolicyholderException>(
            async () => await failingRepository.FindPolicyholderAsync(
                "TEST-SSN",
                "TEST-POLICY",
                "Test",
                "Policyholder",
                "1990-01-15",
                "12345"));
    }

    [Test]
    public void FindPolicyholderByIdAsync_WhenDatabaseQueryFails_ThrowsCouldNotFindPolicyholderException()
    {
        var options = new DbContextOptionsBuilder<InsurancePortalDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        using var failingContext =
            new FailingPolicyholderDbContext(options);

        var failingRepository = new SqlPolicyholderRepository(
            failingContext,
            NullLogger<SqlPolicyholderRepository>.Instance);
        Assert.ThrowsAsync<CouldNotFindPolicyholderException>(
            async () => await failingRepository.FindPolicyholderByIdAsync(
                Guid.NewGuid()));
    }

    private static Policyholder CreatePolicyholder()
    {
        return new Policyholder
        {
            Id = Guid.NewGuid(),
            SSN = "TEST-SSN-123",
            PolicyNumber = "TEST-POLICY-123",
            FirstName = "Test",
            LastName = "Policyholder",
            DateOfBirth = "1990-01-15",
            ZipCode = "12345"
        };
    }

    private sealed class FailingPolicyholderDbContext
        : InsurancePortalDbContext
    {
        public FailingPolicyholderDbContext(
            DbContextOptions<InsurancePortalDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            throw new InvalidOperationException(
                "Simulated database query failure.");
        }
    }
}