using Timekeeper.Domain.Common;
using Timekeeper.Domain.Entities;

namespace Timekeeper.Domain.Events.UserConfigs;

public class UserConfigCompletedEvent : BaseEvent
{
    public UserConfigCompletedEvent(UserConfig userConfig)
    {
        UserConfig = userConfig;
    }

    public UserConfig UserConfig { get; }
}