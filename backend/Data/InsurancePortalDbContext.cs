using InsurancePortalRegistration.Entities;
using Microsoft.EntityFrameworkCore;

namespace InsurancePortalRegistration.Data;

public class InsurancePortalDbContext : DbContext
{
    public InsurancePortalDbContext(
        DbContextOptions<InsurancePortalDbContext> options): base(options)
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

        modelBuilder.Entity<Account>()
            .HasOne<Policyholder>()
            .WithMany()
            .HasForeignKey(account => account.PolicyholderId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<ContactInformation>()
            .HasOne<Policyholder>()
            .WithMany()
            .HasForeignKey(contact => contact.PolicyholderId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<SecurityInformation>()
            .HasOne<Policyholder>()
            .WithMany()
            .HasForeignKey(security => security.PolicyholderId)
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