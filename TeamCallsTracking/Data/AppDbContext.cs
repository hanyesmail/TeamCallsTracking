using TeamCallsTracking.Data.Models;

namespace TeamCallsTracking.Data;

using Microsoft.EntityFrameworkCore;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    // ----- Database Entities ----- //
    public DbSet<EmployeeTitle>  EmployeeTitles { get; set; }
    public DbSet<Employee>  Employees { get; set; }
    
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(AppDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}