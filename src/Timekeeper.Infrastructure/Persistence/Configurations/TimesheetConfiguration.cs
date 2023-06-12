using Timekeeper.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Timekeeper.Infrastructure.Persistence.Configurations;

public class TimesheetConfiguration : IEntityTypeConfiguration<Timesheet>
{
    public void Configure(EntityTypeBuilder<Timesheet> builder)
    {
        builder.Property(t => t.Month)
            .IsRequired()
            .HasAnnotation("ValidationMessage", "Month is required.");

        builder.Property(t => t.Year)
            .IsRequired()
            .HasAnnotation("ValidationMessage", "Year is required.");

        builder.Property(t => t.TotalHours)
            .IsRequired()
            .HasAnnotation("ValidationMessage", "TotalHours is required.");

        builder.HasMany(t => t.Activity)
            .WithOne(a => a.Timesheet)
            .HasForeignKey(a => a.TimesheetId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}