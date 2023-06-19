using Timekeeper.Domain.Common;
using Timekeeper.Domain.ValueObjects.Configs;

namespace Timekeeper.Domain.Entities;

public class UserConfigs : BaseAuditableEntity
{
    public JiraParameters? JiraParameters { get; set; }

    public AzDevOpsParameters? AzDevOpsParameters { get; set; }
}