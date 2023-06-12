using Timekeeper.Domain.Common;
using Timekeeper.Domain.Entities;

namespace Timekeeper.Domain.Events.Activities;

public class ActivityCompletedEvent : BaseEvent
{
    public ActivityCompletedEvent(Activity activity)
    {
        Activity = activity;
    }

    public Activity Activity { get; }
}