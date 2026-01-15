using ChoreJamming.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ChoreJamming.Infrastructure;

public class ChoreDbContext : DbContext
{
    public ChoreDbContext(DbContextOptions<ChoreDbContext> options) : base(options) { }
    
    public DbSet<ChoreHistory> ChoreHistories { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlite("Data Source=chorejamming.db");
        }
    }

    protected override void OnModelCreating(ModelBuilder b)
    {
        b.Entity<ChoreHistory>(e =>
        {
            e.HasKey(k => k.Id);
            e.Property(k => k.Id).ValueGeneratedOnAdd();
            e.Property(k => k.SongTitle)
                .HasMaxLength(128)
                .IsRequired();
            e.Property(k => k.ChoreName)
                .HasMaxLength(128)
                .IsRequired();
            e.Property(k => k.Date);
            e.Property(k => k.Rating)
                .HasDefaultValue(0)
                .HasMaxLength(5);
        });
    }
}

/*
dotnet ef migration add Init --project ..\ChoreJamming.Infrastrucure\ -- startup-project

dotnet ef migrations add InitialCreate --project ChoreJamming.Infrastructure --startup-project ChoreJamming.Client

dotnet ef database update --project ChoreJamming.Infrastructure --startup-project ChoreJamming.Client
  --project MyApp.Data \
  --startup-project MyApp.Api
*/

/*
 *Commands
 * dotnet list package | findstr EntityFrameworkCore -> Find Versions and Package 
 *
 * 
 */