using Timekeeper.Domain.Common;
using Timekeeper.Domain.Entities;

namespace Timekeeper.Domain.Events.Timesheets;

public class ActivityCreatedEvent : BaseEvent
{
    public ActivityCreatedEvent(Activity activity)
    {
        Activity = activity;
    }

    public Activity Activity { get; }
}