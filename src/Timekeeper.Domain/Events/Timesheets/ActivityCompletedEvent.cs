using Timekeeper.Domain.Common;
using Timekeeper.Domain.Entities;

namespace Timekeeper.Domain.Events.Timesheets;

public class ActivityCompletedEvent : BaseEvent
{
    public ActivityCompletedEvent(Activity activity)
    {
        Activity = activity;
    }

    public Activity Activity { get; }
}