using InsurancePortalRegistration.Models;
using Microsoft.EntityFrameworkCore;

namespace InsurancePortalRegistration.Data;

public class InsurancePortalDbContext : DbContext
{
    public InsurancePortalDbContext(DbContextOptions<InsurancePortalDbContext> options): base(options)
    {
        
    }

    public DbSet<Policyholder> Policyholders { get; set; }

    public DbSet<Account> Accounts { get; set; }

    public DbSet<ContactInformation> Contacts { get; set; }

    public DbSet<SecurityInformation> SecurityInformation { get; set; }


}