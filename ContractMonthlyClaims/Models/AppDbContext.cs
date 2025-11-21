using ContractMonthlyClaims.Models;
using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Claim> Claims { get; set; }
    public DbSet<ClaimItem> ClaimItems { get; set; }
    public DbSet<Document> Documents { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configure relationships
        modelBuilder.Entity<Claim>()
            .HasMany(c => c.ClaimItems)
            .WithOne(ci => ci.Claim)
            .HasForeignKey(ci => ci.ClaimId);

        modelBuilder.Entity<Claim>()
            .HasMany(c => c.Documents)
            .WithOne(d => d.Claim)
            .HasForeignKey(d => d.ClaimId);

        // Seed data (optional)
        modelBuilder.Entity<User>().HasData(
            new User { Id = 1, Username = "hradmin", Password = "Admin123!", FirstName = "System", LastName = "Admin", Email = "hr@cmcs.local", HourlyRate = 0, Role = Role.HR },
            new User { Id = 2, Username = "lect1", Password = "Pass123!", FirstName = "John", LastName = "Smith", Email = "john.smith@cmcs.local", HourlyRate = 50, Role = Role.Lecturer },
            new User { Id = 3, Username = "lect2", Password = "Pass123!", FirstName = "Emily", LastName = "Chen", Email = "emily.chen@cmcs.local", HourlyRate = 50, Role = Role.Lecturer },
            new User { Id = 4, Username = "coord1", Password = "Pass123!", FirstName = "Jane", LastName = "Miller", Email = "jane.miller@cmcs.local", HourlyRate = 0, Role = Role.Coordinator },
            new User { Id = 5, Username = "man1", Password = "Pass123!", FirstName = "Sam", LastName = "Newman", Email = "sam.newman@cmcs.local", HourlyRate = 0, Role = Role.Manager }
        );
    }
}