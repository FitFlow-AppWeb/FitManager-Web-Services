using Microsoft.EntityFrameworkCore;
using FitManager_Web_Services.Members.Domain.Model.Aggregates;

namespace FitManager_Web_Services.Shared.Infrastructure.Persistence.EFC.Configuration;

public class AppDbContext : DbContext
{
    public DbSet<Member> Members { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        // Aquí configurarás las entidades
    }
}