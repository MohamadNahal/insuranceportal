using InsurancePortalRegistration.Entities;
using Microsoft.EntityFrameworkCore;

namespace InsurancePortalRegistration.Data;

public class InsurancePortalDbContext : DbContext
{
    public InsurancePortalDbContext(
        DbContextOptions<InsurancePortalDbContext> options) : base(options)
    {
    }

    public DbSet<Policyholder> Policyholders { get; set; }

    public DbSet<Account> Accounts { get; set; }

    public DbSet<ContactInformation> Contacts { get; set; }

    public DbSet<SecurityInformation> SecurityInformation { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>()
            .HasIndex(account => account.Username)
            .IsUnique();

        modelBuilder.Entity<Account>()
            .HasIndex(account => account.PolicyholderId)
            .IsUnique();

        modelBuilder.Entity<ContactInformation>()
            .HasIndex(contact => contact.PolicyholderId)
            .IsUnique();

        modelBuilder.Entity<SecurityInformation>()
            .HasIndex(security => security.PolicyholderId)
            .IsUnique();

        modelBuilder.Entity<Account>()
            .HasOne(account => account.Policyholder)
            .WithOne(policyholder => policyholder.Account)
            .HasForeignKey<Account>(account => account.PolicyholderId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<ContactInformation>()
            .HasOne(contact => contact.Policyholder)
            .WithOne(policyholder => policyholder.ContactInformation)
            .HasForeignKey<ContactInformation>(
                contact => contact.PolicyholderId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<SecurityInformation>()
            .HasOne(security => security.Policyholder)
            .WithOne(policyholder => policyholder.SecurityInformation)
            .HasForeignKey<SecurityInformation>(
                security => security.PolicyholderId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Policyholder>()
            .ToTable("Policyholders", table =>
            {
                table.HasCheckConstraint(
                    "CK_Policyholders_SSN",
                    "SSN NOT LIKE '%[^0-9]%'");

                table.HasCheckConstraint(
                    "CK_Policyholders_ZipCode",
                    "ZipCode NOT LIKE '%[^0-9]%'");
            });

        modelBuilder.Entity<ContactInformation>()
            .ToTable("Contacts", table =>
            {
                table.HasCheckConstraint(
                    "CK_Contacts_ZipCode",
                    "ZipCode NOT LIKE '%[^0-9]%'");
            });
    }
}