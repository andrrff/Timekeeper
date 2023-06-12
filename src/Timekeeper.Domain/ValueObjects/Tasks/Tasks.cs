using Timekeeper.Domain.Common;

namespace Timekeeper.Domain.ValueObjects.Tasks;

public class Tasks : ValueObject
{
    public IEnumerable<TaskItem> Items { get; private set; } = default!;

    public Tasks(IEnumerable<TaskItem> items)
    {
        Items = items;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Items;
    }
}