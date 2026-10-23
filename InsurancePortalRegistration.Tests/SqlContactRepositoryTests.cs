using InsurancePortalRegistration.Data;
using InsurancePortalRegistration.Entities;
using InsurancePortalRegistration.Exceptions;
using InsurancePortalRegistration.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;

namespace InsurancePortalRegistration.Tests;

public class SqlContactRepositoryTests
{
    [Test]
    public async Task AddContactAsync_WhenContactIsValid_SavesContact()
    {
        var options = new DbContextOptionsBuilder<InsurancePortalDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        await using var context = new InsurancePortalDbContext(options);

        var repository = new SqlContactRepository(
            context,
            NullLogger<SqlContactRepository>.Instance);

        var contact = new ContactInformation
        {
            Id = Guid.NewGuid(),
            PolicyholderId = Guid.NewGuid(),
            Email = "test@example.com",
            Phone = "1234567890",
            StreetAddress = "10 Test Street",
            City = "Test City",
            State = "CA",
            ZipCode = "12345",
            Country = "USA"
        };

        await repository.AddContactAsync(contact);

        var savedContact = await context.Contacts
            .SingleOrDefaultAsync(c => c.Id == contact.Id);

        Assert.That(savedContact, Is.Not.Null);
        Assert.That(savedContact!.Email, Is.EqualTo("test@example.com"));
        Assert.That(savedContact.PolicyholderId,
            Is.EqualTo(contact.PolicyholderId));
    }
    
[Test]
public async Task AddContactAsync_WhenSavingFails_ThrowsCouldNotAddContactException()
{

    var options = new DbContextOptionsBuilder<InsurancePortalDbContext>()
        .UseInMemoryDatabase(Guid.NewGuid().ToString())
        .Options;

    await using var context = new FailingSaveChangesDbContext(options);

    var repository = new SqlContactRepository(
        context,
        NullLogger<SqlContactRepository>.Instance);

    var contact = new ContactInformation
    {
        Id = Guid.NewGuid(),
        PolicyholderId = Guid.NewGuid(),
        Email = "test@example.com",
        Phone= "1234567890",
        StreetAddress = "10 Test Street",
        City = "Test City",
        State = "CA",
        ZipCode = "12345",
        Country = "USA"
    };

    Assert.ThrowsAsync<CouldNotAddContactException>(
        async () => await repository.AddContactAsync(contact));
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