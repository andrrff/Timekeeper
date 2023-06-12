using Timekeeper.Domain.Common;
using Timekeeper.Domain.Entities;

namespace Timekeeper.Domain.Events.Activities;

public class ActivityDeletedEvent : BaseEvent
{
    public ActivityDeletedEvent(Activity activity)
    {
        Activity = activity;
    }

    public Activity Activity { get; }
}