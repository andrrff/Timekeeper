using Timekeeper.Domain.Common;
using Timekeeper.Domain.Entities;

namespace Timekeeper.Domain.Events.UserConfigs;

public class UserConfigCreatedEvent : BaseEvent
{
    public UserConfigCreatedEvent(UserConfig userConfig)
    {
        UserConfig = userConfig;
    }

    public UserConfig UserConfig { get; }
}