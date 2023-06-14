using Timekeeper.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Timekeeper.Infrastructure.Persistence.Configurations;

public class ActivityConfiguration : IEntityTypeConfiguration<Activity>
{
    public void Configure(EntityTypeBuilder<Activity> builder)
    {
        builder.Property(a => a.TimesheetId)
            .IsRequired()
            .HasAnnotation("ValidationMessage", "TimesheetId is required.");

        builder.Property(a => a.Note)
            .HasMaxLength(200)
            .HasAnnotation("ValidationMessage", "Note must not exceed 200 characters.");

        builder.HasOne(a => a.Timesheet)
            .WithMany(t => t.Activities)
            .HasForeignKey(a => a.TimesheetId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.OwnsOne(a => a.TaskItem, taskItem =>
        {
            taskItem.Property(ti => ti.Id)
                .IsRequired()
                .HasMaxLength(20)
                .HasAnnotation("ValidationMessage", "TaskItem.Id is required.");

            taskItem.Property(ti => ti.Title)
                .IsRequired()
                .HasAnnotation("ValidationMessage", "Title is required.");

            taskItem.Property(ti => ti.Link)
                .IsRequired()
                .HasAnnotation("ValidationMessage", "Link is required.");

            taskItem.Property(ti => ti.Description)
                .HasMaxLength(200)
                .HasAnnotation("ValidationMessage", "TaskItem.Description must not exceed 200 characters.");

            taskItem.Property(ti => ti.TimeToBeSpent)
                .IsRequired()
                .HasAnnotation("ValidationMessage", "TimeToBeSpent is required.");

            taskItem.Property(ti => ti.TimeSpent)
                .IsRequired()
                .HasAnnotation("ValidationMessage", "TimeSpent is required.");

            taskItem.Property(ti => ti.Origin)
                .IsRequired()
                .HasAnnotation("ValidationMessage", "Origin is required.");

            taskItem.Property(ti => ti.Type)
                .IsRequired()
                .HasAnnotation("ValidationMessage", "Type is required.");

            taskItem.Property(ti => ti.StartDate)
                .HasAnnotation("ValidationMessage", "StartDate must be a valid date.");

            taskItem.Property(ti => ti.DueDate)
                .HasAnnotation("ValidationMessage", "DueDate must be a valid date.");
        });
    }
}