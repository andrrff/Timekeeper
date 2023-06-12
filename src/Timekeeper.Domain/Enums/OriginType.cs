using System.ComponentModel;

namespace Timekeeper.Domain.Enums;

public enum OriginType
{
    [Description("None")]
    None        = 0,
    [Description("Jira")]
    Jira        = 1,
    [Description("Azure DevOps")]
    AzureDevOps = 2,
    [Description("Git")]
    Git         = 3,
    [Description("TFS")]
    TFS         = 4,
    [Description("Other")]
    Other       = 5
}