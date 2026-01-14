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
}