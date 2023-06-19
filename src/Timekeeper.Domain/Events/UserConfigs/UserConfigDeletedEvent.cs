using Timekeeper.Domain.Common;
using Timekeeper.Domain.Entities;

namespace Timekeeper.Domain.Events.UserConfigs;

public class UserConfigDeletedEvent : BaseEvent
{
    public UserConfigDeletedEvent(UserConfig userConfig)
    {
        UserConfig = userConfig;
    }

    public UserConfig UserConfig { get; }
}