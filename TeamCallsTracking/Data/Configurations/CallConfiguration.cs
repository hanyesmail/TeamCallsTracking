using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeamCallsTracking.Data.Models;

namespace TeamCallsTracking.Data.Configurations;

public class CallConfiguration : IEntityTypeConfiguration<Call>
{
    public void Configure(EntityTypeBuilder<Call> builder)
    {
        builder.HasOne(c => c.Status)
            .WithMany(s => s.Calls)
            .HasForeignKey(c => c.CallStatusId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasOne(c => c.EmployeeData)
            .WithMany(s => s.Calls)
            .HasForeignKey(c => c.EmployeeId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasOne(c => c.ClientData)
            .WithMany(s => s.Calls)
            .HasForeignKey(c => c.ClientId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}