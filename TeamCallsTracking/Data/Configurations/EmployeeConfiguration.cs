using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeamCallsTracking.Data.Models;

namespace TeamCallsTracking.Data.Configurations;

public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.HasOne(e => e.EmployeeTitle)
            .WithMany(et => et.Employees)
            .HasForeignKey(e => e.EmployeeTitleId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}