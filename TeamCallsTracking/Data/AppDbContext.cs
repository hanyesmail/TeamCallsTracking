using TeamCallsTracking.Data.Models;

namespace TeamCallsTracking.Data;

using Microsoft.EntityFrameworkCore;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    // ----- Database Entities ----- //
    public DbSet<EmployeeTitle> EmployeeTitles { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Call> Calls { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<CallStatus> CallStatuses { get; set; }
    
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(AppDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}