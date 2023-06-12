using System.ComponentModel;

namespace Timekeeper.Domain.Enums;

public enum TaskType
{
    [Description("None")]
    None        = 0,
    [Description("Development")]
    Development = 1,
    [Description("Bug")]
    Bug         = 2,
    [Description("Improvement")]
    Improvement = 3,
    [Description("Maintenance")]
    Maintenance = 4,
    [Description("Meeting")]
    Meeting     = 5,
    [Description("Training")]
    Training    = 6,
    [Description("Other")]
    Other       = 7
}