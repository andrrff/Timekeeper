using Timekeeper.Domain.Common;

namespace Timekeeper.Domain.Events.UserConfigs;

public class UserConfigCompletedEvent : BaseEvent
{
    public UserConfigCompletedEvent(UserConfigs userConfigs)
    {
        UserConfigs = userConfigs;
    }

    public UserConfigs UserConfigs { get; }
}