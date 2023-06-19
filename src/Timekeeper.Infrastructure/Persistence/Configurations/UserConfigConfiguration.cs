using Timekeeper.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Timekeeper.Infrastructure.Persistence.Configurations;

public class UserConfigConfiguration : IEntityTypeConfiguration<UserConfig>
{
    public void Configure(EntityTypeBuilder<UserConfig> builder)
    {
        builder.Property(uc => uc.JiraParameters)
            .HasAnnotation("ValidationMessage", "JiraParameters is required.");

        builder.Property(uc => uc.AzDevOpsParameters)
            .HasAnnotation("ValidationMessage", "AzDevOpsParameters is required.");
    }
}